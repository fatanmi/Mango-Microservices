using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class UserDto : LoginUserDto
    {
        [DataType(DataType.Text)]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }

    public class LoginUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(
            15,
            ErrorMessage = "Your Password is limited to {2} to {1} characters",
            MinimumLength = 5
        )]
        public string Password { get; set; }
    }

    public class CreateUserDto : LoginUserDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }
    }

    public class AssignRole
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string RoleName { get; set; }
    }
}
