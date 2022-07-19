using IndentityService.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IndentityService.Api.Services
{
    public class IdentityService : IIdentityService
    {
        public Task<LoginResponseModel> Login(LoginRequestModel loginRequestModel)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,loginRequestModel.UserName),
                new Claim(ClaimTypes.NameIdentifier,"Naib residov")
            }
            .AsEnumerable();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Salam necesen ne var ne "));
            var creads = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expries = DateTime.Now.AddDays(10);
            var toke = new JwtSecurityToken(null, null, claims: claims, expires: expries, signingCredentials: creads, notBefore: DateTime.Now);
            var encodingJwt = new JwtSecurityTokenHandler().WriteToken(toke);
           
            var response = new LoginResponseModel()
            {
                UserToken = encodingJwt,
                UserName = loginRequestModel.UserName,
            };
            return Task.FromResult(response);
        }
    }
}
