using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Mango.Web.Implementation.IService;
using Mango.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mango.Web.Models.ViewModel;

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

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            try
            {
                _LoginResponseDto = await _AuthService.LoginAsync(loginUserDto);
                if (_LoginResponseDto.IsSuccess)
                {
                    _TokenService.SetToken(_LoginResponseDto.Token); // Set Token
                    await AuthenticateUserWithIdentity(_LoginResponseDto);
                    TempData["success"] = @"Welcome, " + loginUserDto.Email;
                    return RedirectToAction("CouponIndex", "Coupon");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, _LoginResponseDto.Message);
                    TempData["Error"] = _LoginResponseDto.Message;
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



            // ViewBag.ErrorMessage = _LoginResponseDto?.Message;
            return View(loginUserDto);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel regView)
        {

            if (!ModelState.IsValid)
            {
                return View(new RegisterUserViewModel());
            }

            CreateUserDto createUserDto = regView.User;

            try
            {
                var responseDto = await _AuthService.RegisterAsync(createUserDto);

                if (responseDto.IsSuccess)
                {
                    TempData["success"] = "User created successfully";
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    TempData["Error"] = responseDto.Message;
                    ModelState.AddModelError("", responseDto.Message);
                    return View(new RegisterUserViewModel() { User = createUserDto });
                }

            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            }

            return View(new RegisterUserViewModel());
        }

        [HttpGet]
        public IActionResult Register()
        {


            RegisterUserViewModel registerUserViewModel = new RegisterUserViewModel();

            //return View(createUserDto);
            return View(registerUserViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _TokenService.RemoveToken();
            return RedirectToAction("Index", "Home");
        }

        public async Task AuthenticateUserWithIdentity(LoginResponseDto user)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = new JwtSecurityToken(user.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)?.Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sid, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sid)?.Value));

            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value));

            identity.AddClaims(jwt.Claims
                .Where(x => x.Type.StartsWith("http://schemas.microsoft.com/ws/2008/06/identity/claims/role"))
                .Select(claim => new Claim(ClaimTypes.Role, claim.Value)));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
