using Newtonsoft.Json;
using OnlineOrder.Web.Models.Dtos;
using OnlineOrder.Web.Services.IServices;
using System.Text;
using System.Text.Json.Serialization;

namespace OnlineOrder.Web.Services
{
    public class BaseServices : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseServices(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                using HttpClient client = _httpClientFactory.CreateClient("OnlineOrderAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                if(withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDto.URL);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDto.ApiType)
                {
                    case Utility.ApiRequestType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case Utility.ApiRequestType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case Utility.ApiRequestType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);
                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new ResponseDto() { IsSuccesful = false, Message = "Not Found" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new ResponseDto() { IsSuccesful = false, Message = "Access Denied" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new ResponseDto() { IsSuccesful = false, Message = "Unauthorized" };
                    case System.Net.HttpStatusCode.BadRequest:
                        return new ResponseDto() { IsSuccesful = false, Message = "Bad request" };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new ResponseDto() { IsSuccesful = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    Message = ex.Message,
                    IsSuccesful = false,
                };
            }
        }
    }
}
