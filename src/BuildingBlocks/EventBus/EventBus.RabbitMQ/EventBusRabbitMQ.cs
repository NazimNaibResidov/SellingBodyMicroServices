using EventBus.Base;
using EventBus.Base.Abstrasctions;
using EventBus.Base.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;

namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : BaseEventBus
    {
        RabbitMQPersistenConnection persistenConnection;
        private readonly IConnectionFactory connectionFactory;
        private readonly IModel consumerChannel;
        public EventBusRabbitMQ(IServiceProvider serviceProvider, IEventBusSubscriptionManager subManager, EventBusConfig config) : base(serviceProvider, subManager, config)
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
            persistenConnection = new RabbitMQPersistenConnection(connectionFactory,config.ConnectionRetryCount);
        }
        private IModel CreateConsumerChannel()
        {
            if (!persistenConnection.IsConnection)
            {
                persistenConnection.TryConnect();
            }
            var channel = persistenConnection.CreateModel();
            channel.ExchangeDeclare(EventBusConfig., "direct");
            return channel;
        }
        public override void Publish(IntegrationEvent @event)
        {
            
        }

        public override void SubScribe<T, TH>()
        {
            var eventName = typeof(T).Name;
            eventName = ProccessEventName(eventName);
            if (sum)
            {

            }
        }

        public override void UnSubScribe<T, TH>()
        {
            
        }
    }
}
