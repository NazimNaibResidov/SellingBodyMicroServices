using EventBus.Base;
using EventBus.Base.Abstrasctions;
using EventBus.Factory;
using EventBus.UnitTest.Events.EventHandler;
using EventBus.UnitTest.Events.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventBus.UnitTest
{
    public class EventBusTests
    {
        private ServiceCollection service;

        [SetUp]
        public void Setup()
        {
            service = new ServiceCollection();
            service.AddLogging(x => x.AddConsole());
        }

        [Test]
        public void subscribe_on_rabbitMq_test()
        {
            service.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new()
                {
                    ConnectionRetryCount = 5,
                    SubscriptionClinetAppName = "Naib",
                    DefultTopicName = "NaibResidov",
                    eventBusType = EventBusType.RabbitMq,
                    EventNameSuffix = "IntegrationEvent"
                };
                return EventBusFactory.Create(config, sp);
            });
            var sp = service.BuildServiceProvider();
            var evntBus = sp.GetRequiredService<IEventBus>();
            evntBus.Subscribe<OrderCreateIntegrationEvent, OrderCreateIntegrationEventHandler>();
            Assert.Pass();
        }
    }
}