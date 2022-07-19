using EventBus.Base;
using EventBus.Base.Abstrasctions;
using EventBus.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace NotoificationService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ServiceCollection service = new ServiceCollection();
            ConfiugerService(service);
        }

        private static void ConfiugerService(ServiceCollection service)
        {
            service.AddLogging(config =>
            {
                config.AddConsole();
            });
            service.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig confog = new()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "EventBusTask",
                    SubscriptionClinetAppName = "NotificationService",
                    eventBusType = EventBusType.RabbitMq
                };
                return EventBusFactory.Create(confog, sp);
            });
        }
    }
}