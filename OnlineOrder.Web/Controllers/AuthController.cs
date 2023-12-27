using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OnlineOrder.Web.Models.Dtos;
using OnlineOrder.Web.Services.IServices;
using OnlineOrder.Web.Utility;

namespace OnlineOrder.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
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

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("CustomError", responseDto.Message);

            return View(loginRequestDto);
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
