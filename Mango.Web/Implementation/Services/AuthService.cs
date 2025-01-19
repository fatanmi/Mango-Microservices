using Mango.Web.Implementation.IService;
using Mango.Web.Models;
using Mango.Web.Utilities;

namespace Mango.Web.Implementation.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService<LoginResponseDto> _baseServLoginResDto;
        private readonly IBaseService<ResponseDto> _baseService;

        public AuthService(IBaseService<LoginResponseDto> baseServLoginResDto, IBaseService<ResponseDto> baseService)
        {
            _baseServLoginResDto = baseServLoginResDto;
            _baseService = baseService;
        }

        public Task<LoginResponseDto> LoginAsync(LoginUserDto loginDto)
        {
            return _baseServLoginResDto.SendAsync(
                new RequestDto()
                {
                    ApiType = SD.ApiType.POST,
                    Url = SD.AuthAPIBase + "/api/Account/login",
                    Data = loginDto,
                }
            );
        }

        public async Task<ResponseDto> RegisterAsync(CreateUserDto createUserDto)
        {
            return await _baseService.SendAsync(
                new RequestDto()
                {
                    ApiType = SD.ApiType.POST,
                    Url = SD.AuthAPIBase + "/api/Account/register",
                    Data = createUserDto,
                }
            );
        }

        public async Task<ResponseDto> AssignRoleAsync(AssignRole assignRole)
        {
            return await _baseService.SendAsync(
                new RequestDto()
                {
                    ApiType = SD.ApiType.POST,
                    Url = SD.AuthAPIBase + "/api/Account/AssignRole",
                    Data = assignRole,
                }
            );
        }

        public async Task<ResponseDto> GetUserAsync(string id)
        {
            return await _baseService.SendAsync(
                new RequestDto
                {
                    ApiType = SD.ApiType.GET,
                    Url = SD.AuthAPIBase + "/api/Account/" + id,
                }
            );
        }

        public async Task<ResponseDto> GetAllUserAsync()
        {
            return await _baseService.SendAsync(
                new RequestDto { ApiType = SD.ApiType.GET, Url = SD.AuthAPIBase + "/api/Account" }
            );
        }

        public async Task<ResponseDto> DeleteUserAsync(string id)
        {
            return await _baseService.SendAsync(
                new RequestDto
                {
                    ApiType = SD.ApiType.DELETE,
                    Url = SD.AuthAPIBase + "/api/Account/" + id,
                }
            );
        }
    }
}
