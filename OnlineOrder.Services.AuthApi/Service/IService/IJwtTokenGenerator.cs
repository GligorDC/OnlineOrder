using OnlineOrder.Services.AuthApi.Models;

namespace OnlineOrder.Services.AuthApi.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
