using OnlineOrder.Web.Utility;

namespace OnlineOrder.Web.Models.Dtos
{
    public class RequestDto
    {
        public ApiRequestType ApiType { get; set; } = ApiRequestType.GET;
        public string URL { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
