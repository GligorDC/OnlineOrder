using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OnlineOrder.Web.Models.Dtos;
using OnlineOrder.Web.Services.IServices;
using OnlineOrder.Web.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineOrder.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();

            return View(loginRequestDto);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roles = new List<SelectListItem>()
            {
                new SelectListItem{Text=Constants.Roles.Admin, Value= Constants.Roles.Admin},
                new SelectListItem{Text=Constants.Roles.Customer, Value= Constants.Roles.Customer},
            };
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            var responseDto = await _authService.RegisterAsync(registrationRequestDto);
            ResponseDto assisgnedRoleRsponseDto;

            if (responseDto != null && responseDto.IsSuccesful)
            {
                if (string.IsNullOrEmpty(registrationRequestDto.RoleName))
                {
                    registrationRequestDto.RoleName = Constants.Roles.Customer;
                }
                assisgnedRoleRsponseDto = await _authService.AssignRoleAsync(registrationRequestDto);
                if (assisgnedRoleRsponseDto != null && assisgnedRoleRsponseDto.IsSuccesful)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
                TempData["error"] = assisgnedRoleRsponseDto.Message ?? "Registration Failed";
            }

            var roles = new List<SelectListItem>()
            {
                new SelectListItem{Text=Constants.Roles.Admin},
                new SelectListItem{Text=Constants.Roles.Customer},
            };
            ViewBag.Roles = roles;

            return View(registrationRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var responseDto = await _authService.LoginAsync(loginRequestDto);

            if (responseDto != null && responseDto.IsSuccesful)
            {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                await SignInUser(loginResponseDto);

                _tokenProvider.SetToken(loginResponseDto.Token);

                return RedirectToAction("Index", "Home");
            }
            TempData["error"] = responseDto.Message ?? "Login Failed";

            return View(loginRequestDto);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();

            return RedirectToAction("Index","Home");
        }

        private async Task SignInUser(LoginResponseDto loginRequestDto)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(loginRequestDto.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(user => user.Type == JwtRegisteredClaimNames.Email)?.Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(user => user.Type == JwtRegisteredClaimNames.Sub)?.Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(user => user.Type == JwtRegisteredClaimNames.Name)?.Value));

            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(user => user.Type == JwtRegisteredClaimNames.Email)?.Value));

            identity.AddClaim(new Claim(ClaimTypes.Role,
               jwt.Claims.FirstOrDefault(user => user.Type == "role")?.Value));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
