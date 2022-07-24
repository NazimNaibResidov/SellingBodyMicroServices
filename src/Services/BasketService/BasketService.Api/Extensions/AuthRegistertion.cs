using EventBus.Base;
using EventBus.Base.Abstrasctions;
using EventBus.Factory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BasketService.Api.Extensions
{
    public static class AuthRegistertion
    {
        public static IServiceCollection RegisterionAuth(this IServiceCollection servic, IConfiguration confguration)
        {
            var key = confguration["AuthConfig:secret"];
            var singinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            servic.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = singinKey
                    };
                });
            return servic;
        }
    }
    public static class ConfigurationExtension
    {
        public static void Configuration(this IServiceCollection services,IConfiguration configuration)
        {
            services.RegisterionAuth(configuration);
            services.AddSingleton(sp => sp.ConfigurRedis(configuration));
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
            

        }
    }
}