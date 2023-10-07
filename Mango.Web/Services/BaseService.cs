using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Mango.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("MangoApi");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");

            //token
            message.RequestUri = new Uri(requestDto.Url);
            if (requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage? apiResponse = null;
            message.Method = requestDto.ApiType switch
            {
                Utility.SD.ApiType.POST => HttpMethod.Post,
                Utility.SD.ApiType.PUT => HttpMethod.Put,
                Utility.SD.ApiType.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get
            };

            apiResponse = await httpClient.SendAsync(message);

            try
            {
                return apiResponse.StatusCode switch
                {
                    HttpStatusCode.NotFound => new() { IsSuccess = false, Message = "Not Found" },
                    HttpStatusCode.Forbidden => new() { IsSuccess = false, Message = "Access Denied" },
                    HttpStatusCode.Unauthorized => new() { IsSuccess = false, Message = "Unauthorized" },
                    HttpStatusCode.InternalServerError => new() { IsSuccess = false, Message = "Internal Server Error" },
                    _ => JsonConvert.DeserializeObject<ResponseDto>(await apiResponse.Content.ReadAsStringAsync())
                };
            }
            catch (Exception ex)
            {
                return  new ResponseDto()
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
            }
        }
    }
}
