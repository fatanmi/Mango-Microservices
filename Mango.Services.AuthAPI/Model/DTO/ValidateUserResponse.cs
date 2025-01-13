namespace Mango.Services.AuthAPI.Model.DTO
{
    public class ValidateUserResponse
    {
        public bool isValid { get; set; } = false;
        public UserDto user { get; set; } = null;

    }
}
