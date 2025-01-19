using AutoMapper;
using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Model;
using Mango.Services.AuthAPI.Model.DTO;
using Mango.Services.AuthAPI.Repository.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _UserManager;
        private readonly AuthDBContext _dbContext;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _Mapper;
        private readonly IAuthManager _AuthManager;
        private readonly ResponseDto _responseDto;
        private readonly LoginResponseDto _LoginResponseDto;

        public AccountController(UserManager<ApiUser> userManager, AuthDBContext dbContext, ILogger<AccountController> logger, IMapper mapper, IAuthManager AuthManager)
        {
            _UserManager = userManager;
            _dbContext = dbContext;
            _logger = logger;
            _Mapper = mapper;
            _AuthManager = AuthManager;
            _responseDto = new ResponseDto();
            _LoginResponseDto = new LoginResponseDto();


        }
        [HttpGet]

        [Route("GetById/{Id}", Name = "GetById")]
        public async Task<ResponseDto> Get(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Invalid User Id";
                return _responseDto;
            }
            ApiUser User = await _UserManager.Users.FirstOrDefaultAsync(q => q.Id == Id);
            if (User != null)
            {

                UserDto userDtos = _Mapper.Map<UserDto>(User);
                _responseDto.Result = userDtos;
                return _responseDto;
            }
            else
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "User Does not exist";
                return _responseDto;
            }

        }

        [HttpPost]

        [Route("login")]
        public async Task<LoginResponseDto> Login([FromBody] LoginUserDto User)
        {
            if (!ModelState.IsValid)
            {
                _LoginResponseDto.IsSuccess = false;
                _LoginResponseDto.Message = ModelState.ToString();
                return _LoginResponseDto;
            }
            var isValidUser = await _AuthManager.ValidateUser(User);
            if (!await _AuthManager.ValidateUser(User))
            {
                _LoginResponseDto.IsSuccess = false;
                _LoginResponseDto.Message = "Incorrect Login details";


                return _LoginResponseDto;
            }
            ApiUser UserDetails = await _UserManager.Users.FirstOrDefaultAsync(q => q.Email == User.Email);

            UserDto userDtos = _Mapper.Map<UserDto>(UserDetails);
            string Token = await _AuthManager.CreateToken();
            _LoginResponseDto.Message = "Success";
            _LoginResponseDto.Token = Token;
            _LoginResponseDto.Result = new { userDtos };
            return _LoginResponseDto;
        }

        [HttpPost]
        [Route("AssignRole")]
        public async Task<ResponseDto> AssignRole([FromBody] AssignRole Model)
        {
            if (!ModelState.IsValid)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ModelState.ToString();
                return _responseDto;
            }
            if (!await _AuthManager.AssignRole(Model.RoleName, Model.UserEmail))
            {
                _responseDto.IsSuccess = false;
            }

            _responseDto.IsSuccess = true;
            _responseDto.Message = "Success";

            return _responseDto;
        }


        [HttpGet]
        public async Task<ResponseDto> GetUsers()
        {
            try
            {
                List<ApiUser> Users = await _UserManager.Users.ToListAsync();
                _responseDto.Result = _Mapper.Map<List<UserDto>>(Users);
                return _responseDto;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                _responseDto.IsSuccess = false;
                _responseDto.Message = "An unexpected error occurred. Please try again later.";
                return _responseDto;
            }


        }
        [HttpPost("register")]
        public async Task<ResponseDto> Register([FromBody] CreateUserDto userDTO)
        {
            if (!ModelState.IsValid)
            {
                _responseDto.IsSuccess = false;

                _responseDto.Result = ModelState;
                return _responseDto;
            }
            try
            {
                ApiUser UserDetails = _Mapper.Map<ApiUser>(userDTO);
                UserDetails.UserName = UserDetails.Email;
                IdentityResult UserResult = await _UserManager.CreateAsync(UserDetails, userDTO.Password);
                if (!UserResult.Succeeded)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Result = new { Errors = UserResult.Errors.Select(e => e.Description) };
                    return _responseDto;
                }

                IdentityResult RoleResult = await _UserManager.AddToRoleAsync(UserDetails, userDTO.Roles);
                if (!RoleResult.Succeeded)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Result = new { Errors = RoleResult.Errors.Select(e => e.Description) };
                    return _responseDto;
                }

                _responseDto.Message = "Created!";
                _responseDto.Result = userDTO;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                _responseDto.IsSuccess = false;
                _responseDto.Message = "An unexpected error occurred. Please try again later.";
                return _responseDto;
            }

        }
        [Route("{Id}")]
        [HttpDelete]
        public async Task<ResponseDto> Delete(string Id)
        {
            ApiUser User = await _UserManager.FindByIdAsync(Id);

            if (User != null)
            {
                var Result = await _UserManager.DeleteAsync(User);
                if (!Result.Succeeded)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Result = new { Errors = Result.Errors.Select(e => e.Description) };
                    return _responseDto;
                }
                _responseDto.Message = "Deleted!";
                return _responseDto;
            }
            else
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "User not Found";
                return _responseDto;

            }


        }
    }
}
