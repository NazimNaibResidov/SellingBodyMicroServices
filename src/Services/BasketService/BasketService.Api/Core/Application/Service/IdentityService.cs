using BasketService.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BasketService.Api.Core.Application.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetUsers()
        {
            return httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}