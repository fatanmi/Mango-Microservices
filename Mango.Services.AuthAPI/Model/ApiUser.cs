using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Model
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
