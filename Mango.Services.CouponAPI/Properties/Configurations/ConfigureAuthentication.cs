using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Mango.Services.CouponAPI.Properties.Configurations
{
    public static class ConfigureAuthentication
    {
        public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {

            //string issuer = configuration.GetSection("JwtSetting")["Issuer"];
            string issuer = configuration["JwtSetting:Issuer"];
            string audience = configuration["JwtSetting:Audience"];

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

        public static void ConfigureSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] { }
        }
    });


            });

        }


    }
}
