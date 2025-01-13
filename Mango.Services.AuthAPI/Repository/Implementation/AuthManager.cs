using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Model;
using Mango.Services.AuthAPI.Model.DTO;
using Mango.Services.AuthAPI.Repository.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Services.AuthAPI.Repository.Implementation
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApiUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly IConfiguration _Configuration;
        private ApiUser _User;
        private readonly AuthDBContext _DbContext;
        private readonly IConfigurationSection _JWTSettings;

        public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration, AuthDBContext dbContext, RoleManager<IdentityRole> RoleManager)
        {
            _UserManager = userManager;
            _Configuration = configuration;
            _RoleManager = RoleManager;

            _DbContext = dbContext;
            _JWTSettings = _Configuration.GetSection("JwtSetting");
        }

        public async Task<string> CreateToken()
        {
            SigningCredentials SignCredential = GetSigninCredentials();
            IEnumerable<Claim> Claims = await GetClaims();
            JwtSecurityToken TokenOptionsClaim = GenerateTokeOptions(SignCredential, Claims);

            return new JwtSecurityTokenHandler().WriteToken(TokenOptionsClaim);
        }
        private JwtSecurityToken GenerateTokeOptions(SigningCredentials SignCredentials, IEnumerable<Claim> claims)
        {
            JwtSecurityToken Token = new(issuer: _JWTSettings["Issuer"],
                                         audience: _JWTSettings["Audience"],
                                         claims: claims,
                                         expires: DateTime.UtcNow.AddSeconds(Convert.ToDouble(_JWTSettings["Lifetime"])),
                                         signingCredentials: SignCredentials);

            return Token;
        }
        private async Task<IEnumerable<Claim>> GetClaims()
        {
            var Claims = new List<Claim>()
            {
                new (JwtRegisteredClaimNames.Name, _User.UserName?? string.Empty),
                new (JwtRegisteredClaimNames.Email, _User.Email?? string.Empty),
                new (JwtRegisteredClaimNames.Sid, _User.Id?? string.Empty),
            };

            IEnumerable<string> Roles = await _UserManager.GetRolesAsync(_User);
            foreach (string Role in Roles)
            {
                Claims.Add(new(ClaimTypes.Role, Role));
            }
            return Claims;
        }
        private static SigningCredentials GetSigninCredentials()
        {
            string Key = Environment.GetEnvironmentVariable("Key");
            SymmetricSecurityKey Secrete = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            return new SigningCredentials(Secrete, SecurityAlgorithms.HmacSha256);
        }
        public async Task<bool> ValidateUser(LoginUserDto UserDTO)
        {
            _User = await _UserManager.FindByEmailAsync(UserDTO.Email);
            bool Valid = _User != null && await _UserManager.CheckPasswordAsync(_User, UserDTO.Password);

            return Valid;
        }
        public async Task<bool> AssignRole(string RoleName, string UserEmail)
        {

            ApiUser _User = await _UserManager.FindByEmailAsync(UserEmail);

            if (_User != null)
            {
                if (!await _RoleManager.RoleExistsAsync(RoleName))
                {
                    await _RoleManager.CreateAsync(new IdentityRole(RoleName));
                }
                await _UserManager.AddToRoleAsync(_User, RoleName);
                return true;
            }
            return false;
        }

    }
}
