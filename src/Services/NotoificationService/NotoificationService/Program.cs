using EventBus.Base;
using EventBus.Base.Abstrasctions;
using EventBus.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotoificationService.IntegrationEvents.EventHandler;
using NotoificationService.IntegrationEvents.Events;
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
            var sp = service.BuildServiceProvider();
            var subService = sp.GetRequiredService<IEventBus>();
            subService.Subscribe<OrderPaymnetFailedIntegrationEvent, OrderPaymnetFailedIntegrationEventHandlerHandler>();
            subService.Subscribe<OrderPaymnetSuccessIntegrationEvent, OrderPaymnetSuccessIntegrationEventHandler>();
            //sp.GetRequiredService<OrderPaymnetFailedIntegrationEvent, OrderPaymnetFailedIntegrationEventHandlerHandler>();

            Console.WriteLine("Hello World is ruuning!");
            Console.ReadLine();
        }

        private static void ConfiugerService(ServiceCollection service)
        {
            service.AddLogging(config =>
            {
                config.AddConsole();
            });
            service.AddTransient<OrderPaymnetFailedIntegrationEventHandlerHandler>();
            service.AddTransient<OrderPaymnetSuccessIntegrationEventHandler>();
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