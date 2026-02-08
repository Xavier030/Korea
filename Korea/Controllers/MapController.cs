//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http;
//using System.Threading.Tasks;
//
//namespace Korea.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MapController : ControllerBase
//    {
//        private readonly HttpClient _httpClient;
//
//        public MapController(IHttpClientFactory httpClientFactory)
//        {
//            _httpClient = httpClientFactory.CreateClient();
//        }
//
//        [HttpGet("search")]
//        public async Task<IActionResult> Search([FromQuery] string query)
//        {
//            var apiKey = "e49cacaad8124d6eacf5291b89608d336d3a747c7c93e4dd6bc3f74469513a81";
//            var url = $"https://serpapi.com/search.json?engine=google_maps&q={System.Net.WebUtility.UrlEncode(query)}&type=search&api_key={apiKey}";
//            var response = await _httpClient.GetAsync(url);
//            var content = await response.Content.ReadAsStringAsync();
//            return Content(content, "application/json");
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Korea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public MapController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query, [FromQuery] bool useMock = true)
        {
            if (useMock) // 开发模式默认返回模拟数据
            {
                var result = new {
                    place_results = new {
                        title = "景福宫",
                        address = "首尔钟路区",
                        gps_coordinates = new { latitude = 37.5796, longitude = 126.9770 },
                        thumbnail = "https://upload.wikimedia.org/wikipedia/commons/0/0c/Gyeongbokgung_Palace.jpg"
                    }
                };
                return Ok(result);
            }

            // 调用真实 SerpApi（可选）
            var apiKey = "e49cacaad8124d6eacf5291b89608d336d3a747c7c93e4dd6bc3f74469513a81";
            var url = $"https://serpapi.com/search.json?engine=google_maps&q={System.Net.WebUtility.UrlEncode(query)}&type=search&api_key={apiKey}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
    }
}
