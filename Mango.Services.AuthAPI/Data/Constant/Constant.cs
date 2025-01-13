namespace Mango.Services.AuthAPI.Data.Constant
{
    public class Constant
    {
        public static IDictionary<string, (string Name, string NormalizedName)> RoleName = new Dictionary<string, (string Name, string NormalizedName)>
        {
             {"UserRole", ("User", "USER" ) },
             {"AdminRole", ("Admin", "ADMINISTRATOR" ) },

        };
    }
}
