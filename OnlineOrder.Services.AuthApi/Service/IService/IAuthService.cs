using OnlineOrder.Services.AuthApi.Data;
using OnlineOrder.Services.AuthApi.Models.Dtos;

namespace OnlineOrder.Services.AuthApi.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
