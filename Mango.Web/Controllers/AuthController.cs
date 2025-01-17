using System.Net;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Mango.Web.Implementation.IService;
using Mango.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _AuthService;
        private readonly ITokenService _TokenService;
        private ResponseDto _responseDto;
        private LoginResponseDto _LoginResponseDto;
        private readonly ILogger<AuthController> _Logger;

        public AuthController(
            IAuthService authService,
            ITokenService TokenService,
            ILogger<AuthController> Logger
        )
        {
            _AuthService = authService;
            _responseDto = new ResponseDto();
            _LoginResponseDto = new LoginResponseDto();
            _TokenService = TokenService;
            _Logger = Logger;
        }

        public IActionResult Login()
        {
            LoginUserDto loginUserDto = new LoginUserDto();

            return View(loginUserDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _LoginResponseDto = await _AuthService.LoginAsync(loginUserDto);
                    if (_LoginResponseDto.IsSuccess)
                    {
                        _TokenService.SetToken(_LoginResponseDto.Token.ToString()); // Set Token
                        TempData["success"] = @"Welcome, " + loginUserDto.Email;
                        return RedirectToAction("CouponIndex", "Coupon");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, _LoginResponseDto.Message);
                    }
                }
                catch (Exception ex)
                {
                    _Logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                    ModelState.AddModelError(
                        string.Empty,
                        "An unexpected error occurred. Please try again later."
                    );
                }
            }

            // ViewBag.ErrorMessage = _LoginResponseDto?.Message;
            return View(loginUserDto);
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDto createUserDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var responseDto = await _AuthService.RegisterAsync(createUserDto);

                    if (responseDto.IsSuccess)
                    {
                        return RedirectToAction(nameof(Login));
                    }
                    else
                    {
                        ModelState.AddModelError("", responseDto.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            }

            //return View(createUserDto);
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var RoleList = new List<SelectListItem>();

            foreach (var Role in Constants.Constant.RoleName)
            {
                RoleList.Add(
                    new SelectListItem { Text = Role.Value.Name, Value = Role.Value.Name }
                );
            }

            ViewBag.RoleList = RoleList;
            CreateUserDto createUserDto = new CreateUserDto();

            //return View(createUserDto);
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        public async Task<IActionResult> SignInAsync(LoginUserDto user) // Add this method
        {
            var handler = new JwtSecurityTokenHandler();
            var principal = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] { new Claim(ClaimTypes.Name, user.Email) },
                    CookieAuthenticationDefaults.AuthenticationScheme
                )
            );
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new[] { new Claim(ClaimTypes.Name, user.Email) },
                        CookieAuthenticationDefaults.AuthenticationScheme
                    )
                ),
                new AuthenticationProperties { IsPersistent = user.RememberMe }
            );
            return View();
        }
    }
}
