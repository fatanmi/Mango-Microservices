using Mango.Services.AuthAPI.Model.DTO;

namespace Mango.Services.AuthAPI.Repository.Contract
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDto UserDTO);

        Task<string> CreateToken();

        Task<bool> AssignRole(string RoleName, string Email);

    }
}
