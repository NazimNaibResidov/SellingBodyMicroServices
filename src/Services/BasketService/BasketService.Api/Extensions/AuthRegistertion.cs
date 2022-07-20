using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BasketService.Api.Extensions
{
    public static class AuthRegistertion
    {
        public static IServiceCollection RegisterionAuth(IServiceCollection servic, IConfiguration confguration)
        {
            
            var key = confguration["key"];
            var singinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            servic.AddAuthentication(ops =>
            {
                // ops.DefaultAuthenticateScheme=JwtBearerDefaults
            });
            return null;
        }
    }
}