using EventBus.Base;
using EventBus.Base.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Net.Sockets;
using System.Text;

namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : BaseEventBus
    {
        private RabbitMQPersistenConnection persistenConnection;
        private readonly IConnectionFactory connectionFactory;

        private readonly IModel consumerChannel;

        public EventBusRabbitMQ(IServiceProvider serviceProvider, EventBusConfig config) : base(serviceProvider, config)
        {
            if (config.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(config, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                connectionFactory = JsonConvert.DeserializeObject<ConnectionFactory>(connJson);
            }
            else
                connectionFactory = new ConnectionFactory();

            persistenConnection = new RabbitMQPersistenConnection(connectionFactory, config.ConnectionRetryCount);
            consumerChannel = CreateConsumerChannel();
            SubManager.OnEventRemoved += SubManager_OnEventRemoved;
        }

        public override void Publish(IntegrationEvent @event)
        {
            if (!persistenConnection.IsConnection)
            {
                persistenConnection.TryConnect();
            }
            var policy = Policy.Handle<SocketException>()
                   .Or<BrokerUnreachableException>()
                   .WaitAndRetry(eventBusConfig.ConnectionRetryCount, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)), (Ex, time) =>
                   {
                   });
            var eventName = @event.GetType().Name;
            eventName = ProccessEventName(eventName);
            consumerChannel.ExchangeDeclare(eventBusConfig.DefultTopicName, "direct");
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                var Properties = consumerChannel.CreateBasicProperties();
                Properties.DeliveryMode = 2;
                consumerChannel.QueueDeclare(GetSubName(eventName),
                                            true,
                                            false,
                                            false,
                                            null);
                consumerChannel.BasicPublish(eventBusConfig.DefultTopicName, eventName, true, Properties, body);
            });
        }

        public override void SubScribe<T, TH>()
        {
            var eventName = typeof(T).Name;
            eventName = ProccessEventName(eventName);
            if (!SubManager.HasSubScriptionsForEvent(eventName))
            {
                if (!persistenConnection.IsConnection)
                {
                    persistenConnection.TryConnect();
                }
                consumerChannel.QueueDeclare(GetSubName(eventName),
                                             true,
                                             false,
                                             false,
                                             null);
                consumerChannel.QueueBind(GetSubName(eventName),
                                          eventBusConfig.DefultTopicName,
                                          eventName);
            }
            SubManager.AddSubscription<T, TH>();
            StartBasicConsumer(eventName);
        }

        public override void UnSubScribe<T, TH>()
        {
            SubManager.RemoveSubscription<T, TH>();
        }

        #region ::private::

        private IModel CreateConsumerChannel()
        {
            if (!persistenConnection.IsConnection)
            {
                persistenConnection.TryConnect();
            }
            var channel = persistenConnection.CreateModel();
            channel.ExchangeDeclare(eventBusConfig.DefultTopicName, "direct");
            return channel;
        }

        private void StartBasicConsumer(string name)
        {
            if (consumerChannel != null)
            {
                var consumer = new EventingBasicConsumer(consumerChannel);
                consumer.Received += Consumer_Received;
                consumerChannel.BasicConsume(GetSubName(name), false, consumer);
            }
        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            eventName = ProccessEventName(eventName);
            var message = Encoding.UTF8.GetString(e.Body.Span);
            try
            {
                await ProccessEvent(eventName, message);
            }
            catch (Exception)
            {
            }
            consumerChannel.BasicAck(e.DeliveryTag, false);
        }

        private void SubManager_OnEventRemoved(object sender, string _eventName)
        {
            var eventName = ProccessEventName(_eventName);
            if (!persistenConnection.IsConnection)
            {
                persistenConnection.TryConnect();
            }
            consumerChannel.QueueBind(eventName, eventBusConfig.DefultTopicName, eventName);
            if (SubManager.IsEmpty)
            {
                consumerChannel.Close();
            }
        }

        #endregion ::private::
    }
}