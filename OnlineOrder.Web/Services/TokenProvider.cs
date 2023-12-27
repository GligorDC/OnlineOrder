using Newtonsoft.Json.Linq;
using OnlineOrder.Web.Services.IServices;
using OnlineOrder.Web.Utility;

namespace OnlineOrder.Web.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(Constants.TokenCookie);
        }

        public string? GetToken()
        {
           string? token = null;
           bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(Constants.TokenCookie, out token);
           return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
           _contextAccessor.HttpContext?.Response.Cookies.Append(Constants.TokenCookie, token);
        }
    }
}
