using Mango.Web.Implementation.IService;
using Mango.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _AuthService;
        private ResponseDto _responseDto;

        public AuthController(IAuthService authService)
        {
            _AuthService = authService;
            _responseDto = new ResponseDto();
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {

            LoginUserDto loginUserDto = new LoginUserDto();

            return View(loginUserDto);
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDto createUserDto)
        {
            var responDto = await _AuthService.RegisterAsync(createUserDto);

            if (responDto.IsSuccess)
            {
                RedirectToAction(nameof(Login));
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
                RoleList.Add(new SelectListItem { Text = Role.Value.Name, Value = Role.Value.Name });
            }

            ViewBag.RoleList = RoleList;
            CreateUserDto createUserDto = new CreateUserDto();

            //return View(createUserDto);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            return View();
        }

    }
}

