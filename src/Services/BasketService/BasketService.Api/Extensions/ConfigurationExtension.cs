using BasketService.Api.IntegrationEvents.EventHandler;
using EventBus.Base;
using EventBus.Base.Abstrasctions;
using EventBus.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasketService.Api.Extensions
{
    public static class ConfigurationExtension
    {
        public static void Configuration(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterionAuth(configuration);
            services.AddSingleton(sp => sp.ConfigurRedis(configuration));
            services.Services();
            services.AddSingleton<IEventBus>(sp =>
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
            services.AddTransient<OrderCreatedIntegrationEventsHandler>();
        }
    }
}