using Microsoft.AspNetCore.Identity;
using OnlineOrder.Services.AuthApi.Data;
using OnlineOrder.Services.AuthApi.Models;
using OnlineOrder.Services.AuthApi.Models.Dtos;
using OnlineOrder.Services.AuthApi.Service.IService;

namespace OnlineOrder.Services.AuthApi.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(AppDbContext db,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(user => user.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isUserValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (!isUserValid || user == null)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            return new LoginResponseDto()
            {
                User = new UserDto()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                },
                Token = _jwtTokenGenerator.GenerateToken(user),
            };
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToLower(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(user => user.UserName == registrationRequestDto.Email);
                    UserDto userDto = new()
                    {
                        Email = user.Email,
                        Id = user.Id,
                        Name = user.Name,
                        PhoneNumber = user.PhoneNumber,
                    };
                    return "";
                }
                return result.Errors.FirstOrDefault().Description;
            }
            catch (Exception ex)
            {

            }
            return "Error occured";
        }
        public async Task<bool> AssignRole(string email, string roleName) {
            var user = _db.ApplicationUsers.FirstOrDefault(user => user.Email.ToLower() == email.ToLower());
            if(user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user,roleName);
                return true;
            }
            return false;
        }
    }
}
