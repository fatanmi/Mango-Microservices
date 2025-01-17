namespace Mango.Web.Utilities
{
    public class SD
    {
        public static string CouponAPIBase { get; set; } = null;
        public static string AuthAPIBase { get; set; } = null;
        //public static void ConfigureCouponAPIBase(IConfiguration configuration) {

        //    CouponAPIBase = configuration.GetSection("ServiceUrls").Value;
        //}
        public enum ApiType
        {
            GET, POST, PUT, DELETE, TRACE,
        }
    }
}
