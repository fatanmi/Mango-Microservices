using Mango.Web.Implementation.IService;
using Mango.Web.Utilities;

namespace Mango.Web.Implementation.Services
{
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void RemoveToken()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }

        public string GetToken()
        {
            string token = null;
            bool hasToken =
                _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(
                    SD.TokenCookie,
                    out token
                ) ?? false;
            return hasToken ? token : null;
        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
        }
    }
}
