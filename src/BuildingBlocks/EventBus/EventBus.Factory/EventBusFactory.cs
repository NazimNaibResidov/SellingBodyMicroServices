using EventBus.Base;
using EventBus.Base.Abstrasctions;
using EventBus.RabbitMQ;
using System;

namespace EventBus.Factory
{
    public static class EventBusFactory
    {
        public static IEventBus Create(EventBusConfig config, IServiceProvider provider)
        {
            return config.eventBusType switch
            {
                EventBusType.RabbitMq => new EventBusRabbitMQ(provider, config),
            };
        }
    }
}