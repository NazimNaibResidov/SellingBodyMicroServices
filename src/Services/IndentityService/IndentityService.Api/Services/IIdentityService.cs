using IndentityService.Api.Models;
using System.Threading.Tasks;

namespace IndentityService.Api.Services
{
    public interface IIdentityService
    {
        Task<LoginResponseModel> Login(LoginRequestModel loginRequestModel);
    }
}
