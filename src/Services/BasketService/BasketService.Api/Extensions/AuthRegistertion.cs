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
}