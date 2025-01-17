using Mango.Web.Models;

namespace Mango.Web.Implementation.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginAsync(LoginUserDto loginDto);
        Task<ResponseDto> RegisterAsync(CreateUserDto createUserDto);
        Task<ResponseDto> AssignRoleAsync(AssignRole assignRole);
    }
}
