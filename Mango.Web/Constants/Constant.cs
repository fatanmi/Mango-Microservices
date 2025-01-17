
namespace Mango.Web.Constants
{
    public class Constant
    {
        public static IDictionary<string, (string Name, string NormalizedName)> RoleName = new Dictionary<string, (string Name, string NormalizedName)>
        {
             {"UserRole", ("User", "USER" ) },
             {"AdminRole", ("Admin", "ADMINISTRATOR" ) },
             {"CustomerRole", ("Customer", "CUSTOMER" ) },

        };

    }
}
