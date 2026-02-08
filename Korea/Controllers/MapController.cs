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
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var apiKey = "e49cacaad8124d6eacf5291b89608d336d3a747c7c93e4dd6bc3f74469513a81";
            var url = $"https://serpapi.com/search.json?engine=google_maps&q={System.Net.WebUtility.UrlEncode(query)}&type=search&api_key={apiKey}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
    }
}
