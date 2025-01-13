using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Model;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.ServiceExtension
{
    public static class ServiceExtension
    {
        public static void ConfigureIdentity(this IServiceCollection Services)
        {
            IdentityBuilder Builder = Services.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail = true);

            Builder = new IdentityBuilder(Builder.UserType, typeof(IdentityRole), Services);
            Builder.AddEntityFrameworkStores<AuthDBContext>().AddDefaultTokenProviders();

        }
    }
}
