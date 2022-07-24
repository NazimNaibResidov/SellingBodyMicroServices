using BasketService.Api.Core.Application.Service;
using BasketService.Api.Infrastrucutre.Repostorye;
using BasketService.Api.IntegrationEvents.EventHandler;
using BasketService.Api.IntegrationEvents.Events;
using BasketService.Api.Interfaces;
using EventBus.Base.Abstrasctions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;

namespace BasketService.Api.Extensions
{
    public static class RedisRegistertion
    {
        public static ConnectionMultiplexer ConfigurRedis(this IServiceProvider servic, IConfiguration confguration)
        {
            var redisConfig = ConfigurationOptions.Parse(confguration["Redis:connectionString"], true);
            redisConfig.ResolveDns = true;
            return ConnectionMultiplexer.Connect(redisConfig);
        }

        private static void LoadSubscribe(this IServiceProvider servic)
        {
            var eventBus = servic.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrderCreatedIntegrationEvents, OrderCreatedIntegrationEventsHandler>();
        }
    }

    public static class ServiceRestration
    {
        public static IServiceCollection Services(this IServiceCollection servic)
        {
            servic.AddHttpContextAccessor();
            servic.AddScoped<IBasketRepsotory, RedisBasketRepsotory>();
            servic.AddTransient<IIdentityService, IdentityService>();
            return servic;
        }
    }
}