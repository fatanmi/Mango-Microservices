using System.Net;
using System.Text;
using Mango.Web.Implementation.IService;
using Mango.Web.Models;
using Newtonsoft.Json;
using static Mango.Web.Utilities.SD;

namespace Mango.Web.Implementation.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> SendAsync(RequestDto requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                //token

                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(
                        JsonConvert.SerializeObject(requestDto.Data),
                        Encoding.UTF8,
                        "application/json"
                    );
                }

                HttpResponseMessage apiResponse = null;
                message.Method = requestDto.ApiType switch
                {
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                };
                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.InternalServerError:
                        var errorResponse = new ResponseDto
                        {
                            IsSuccess = false,
                            Message = apiResponse.StatusCode.ToString()
                        };

                        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(errorResponse));

                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(apiContent);
                }
            }
            catch (Exception ex)
            {
                return new { IsSuccess = false, Message = ex.Message.ToString() } as T;
            }
        }
    }
}
