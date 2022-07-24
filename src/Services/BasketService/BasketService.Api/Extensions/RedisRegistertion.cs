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
          var redisConfig=  ConfigurationOptions.Parse(confguration["Redis:connectionString"], true);
            redisConfig.ResolveDns = true;
            return ConnectionMultiplexer.Connect(redisConfig);
        }
    }
}