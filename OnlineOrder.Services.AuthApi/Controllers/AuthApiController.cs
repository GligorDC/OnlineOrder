using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineOrder.Services.AuthApi.Models.Dto;
using OnlineOrder.Services.AuthApi.Models.Dtos;
using OnlineOrder.Services.AuthApi.Service.IService;

namespace OnlineOrder.Services.AuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        private ResponseDto _responseDto;
        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var registrationStatus = await  _authService.Register(registrationRequestDto);
            if(!string.IsNullOrEmpty(registrationStatus))
            {
                _responseDto.IsSuccesful = false;
                _responseDto.Message = registrationStatus;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponseDto = await _authService.Login(loginRequestDto);
            if(loginResponseDto.User == null) {
                _responseDto.IsSuccesful = false;
                _responseDto.Message = "The username or password is incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = loginResponseDto;

            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var roleAssignedSuccessful = await _authService.AssignRole(registrationRequestDto.Email, registrationRequestDto.RoleName.ToUpper());
            if (!roleAssignedSuccessful)
            {
                _responseDto.IsSuccesful = false;
                _responseDto.Message = "Error encountered";
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }
    }
}
