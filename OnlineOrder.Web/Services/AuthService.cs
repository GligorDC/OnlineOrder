using OnlineOrder.Web.Models.Dtos;
using OnlineOrder.Web.Services.IServices;
using OnlineOrder.Web.Utility;

namespace OnlineOrder.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.ApiRequestType.POST,
                Data = registrationRequestDto,
                URL = ApiBase.Auth + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.ApiRequestType.POST,
                Data = loginRequestDto,
                URL = ApiBase.Auth + "/api/auth/login"
            });
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.ApiRequestType.POST,
                Data = registrationRequestDto,
                URL = ApiBase.Auth + "/api/auth/register"
            });
        }
    }
}
