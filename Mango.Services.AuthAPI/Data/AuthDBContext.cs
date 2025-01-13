using Mango.Services.AuthAPI.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Mango.Services.AuthAPI.Data
{
    public class AuthDBContext : IdentityDbContext<ApiUser>
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
