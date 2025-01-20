using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mango.Services.CouponAPI.Properties.Configurations
{
    public static class ConfigureAuthentication
    {
        public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {

            string issuer = configuration["JwtSettings:Issuer"];
            string audience = configuration["JwtSettings:Audience"];

            string Key = Environment.GetEnvironmentVariable("Key");
            SymmetricSecurityKey Secrete = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

            services.AddAuthentication(x => { x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = Secrete
                    };
                });

        }
    }
}
