using IndentityService.Api.Models;
using IndentityService.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService service;

        public AuthController(IIdentityService service)
        {
            this.service = service;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequestModel model)
        {
            var result = service.Login(model);
            return Ok(result);
        }
    }
}