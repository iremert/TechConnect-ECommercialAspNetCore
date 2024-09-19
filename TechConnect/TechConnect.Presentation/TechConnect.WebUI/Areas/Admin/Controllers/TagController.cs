using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TechConnect.DtoUI.TagDtos;

namespace TechConnect.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TagController(IHttpClientFactory httpClientFactory)
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


            var request = await client.GetAsync("https://localhost:7237/api/Tag");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var Tags = JsonConvert.DeserializeObject<List<ResultTagDto>>(responseMessage);
                return View(Tags);
            }
            return View();
        }





        [HttpGet]
        public IActionResult AddTag()
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
        public async Task<IActionResult> AddTag(CreateTagDto createTagDto)
        {
            createTagDto.Status = false;


            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(createTagDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
            var responseMessage = await client.PostAsync("https://localhost:7237/api/Tag/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var url = "/Admin/Tag/Index/";
                return Redirect(url);
            }
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> UpdateTag(string id)
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


            var request = await client.GetAsync("https://localhost:7237/api/Tag/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var Tag = JsonConvert.DeserializeObject<GetByIdTagDto>(responseMessage);
                return View(Tag);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTag(GetByIdTagDto Tag)
        {

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(Tag);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
            var responseMessage = await client.PutAsync("https://localhost:7237/api/Tag/" + Tag.ID, stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var url = "/Admin/Tag/Index/";
                return Redirect(url);
            }
            return View();

        }



        public async Task<IActionResult> DeleteTag(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));
            var responseMessage = await client.DeleteAsync($"https://localhost:7237/api/Tag/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var url2 = "/Admin/Tag/Index/";
                return Redirect(url2);
            }
            var url = "/Admin/Tag/Index/";
            return Redirect(url);
        }



        public async Task<IActionResult> DoActiveTag(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Tag/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage2 = await request.Content.ReadAsStringAsync();
                var Tag = JsonConvert.DeserializeObject<GetByIdTagDto>(responseMessage2);
                Tag.Status = true;
                var jsonData = JsonConvert.SerializeObject(Tag);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var responseMessage = await client.PutAsync("https://localhost:7237/api/Tag/" + id, stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var url2 = "/Admin/Tag/Index/";
                    return Redirect(url2);
                }
            }




            var url = "/Admin/Tag/Index/";
            return Redirect(url);
        }

        public async Task<IActionResult> DoPassiveTag(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Tag/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage2 = await request.Content.ReadAsStringAsync();
                var Tag = JsonConvert.DeserializeObject<GetByIdTagDto>(responseMessage2);
                Tag.Status = false;
                var jsonData = JsonConvert.SerializeObject(Tag);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var responseMessage = await client.PutAsync("https://localhost:7237/api/Tag/" + id, stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var url2 = "/Admin/Tag/Index/";
                    return Redirect(url2);
                }
            }




            var url = "/Admin/Tag/Index/";
            return Redirect(url);
        }
    }
}
