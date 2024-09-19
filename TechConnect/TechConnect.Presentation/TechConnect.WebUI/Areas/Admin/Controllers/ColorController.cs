using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TechConnect.DtoUI.ColorDtos;

namespace TechConnect.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ColorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            // Payload kısmına erişim
            var claims = jwtToken.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("AdminPermission"))
            {
                return Unauthorized();
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Color");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var Colors = JsonConvert.DeserializeObject<List<ResultColorDto>>(responseMessage);
                return View(Colors);
            }
            return View();
        }





        [HttpGet]
        public IActionResult AddColor()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            // Payload kısmına erişim
            var claims = jwtToken.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("AdminPermission"))
            {
                return Unauthorized();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddColor(CreateColorDto createColorDto)
        {
            createColorDto.Status = false;


            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(createColorDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
            var responseMessage = await client.PostAsync("https://localhost:7237/api/Color/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var url = "/Admin/Color/Index/";
                return Redirect(url);
            }
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> UpdateColor(string id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            // Payload kısmına erişim
            var claims = jwtToken.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("AdminPermission"))
            {
                return Unauthorized();
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Color/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var Color = JsonConvert.DeserializeObject<GetByIdColorDto>(responseMessage);
                return View(Color);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateColor(GetByIdColorDto Color)
        {

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(Color);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
            var responseMessage = await client.PutAsync("https://localhost:7237/api/Color/" + Color.ID, stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var url = "/Admin/Color/Index/";
                return Redirect(url);
            }
            return View();

        }



        public async Task<IActionResult> DeleteColor(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));
            var responseMessage = await client.DeleteAsync($"https://localhost:7237/api/Color/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var url2 = "/Admin/Color/Index/";
                return Redirect(url2);
            }
            var url = "/Admin/Color/Index/";
            return Redirect(url);
        }



        public async Task<IActionResult> DoActiveColor(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Color/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage2 = await request.Content.ReadAsStringAsync();
                var Color = JsonConvert.DeserializeObject<GetByIdColorDto>(responseMessage2);
                Color.Status = true;
                var jsonData = JsonConvert.SerializeObject(Color);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var responseMessage = await client.PutAsync("https://localhost:7237/api/Color/" + id, stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var url2 = "/Admin/Color/Index/";
                    return Redirect(url2);
                }
            }




            var url = "/Admin/Color/Index/";
            return Redirect(url);
        }

        public async Task<IActionResult> DoPassiveColor(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Color/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage2 = await request.Content.ReadAsStringAsync();
                var Color = JsonConvert.DeserializeObject<GetByIdColorDto>(responseMessage2);
                Color.Status = false;
                var jsonData = JsonConvert.SerializeObject(Color);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var responseMessage = await client.PutAsync("https://localhost:7237/api/Color/" + id, stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var url2 = "/Admin/Color/Index/";
                    return Redirect(url2);
                }
            }




            var url = "/Admin/Color/Index/";
            return Redirect(url);
        }
    }
}
