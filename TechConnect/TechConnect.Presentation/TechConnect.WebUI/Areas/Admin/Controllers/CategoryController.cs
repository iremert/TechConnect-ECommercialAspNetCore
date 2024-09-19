using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TechConnect.DtoUI.CategoryDtos;

namespace TechConnect.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
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


            var request = await client.GetAsync("https://localhost:7237/api/Category");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var Categorys = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(responseMessage);
                return View(Categorys);
            }
            return View();
        }





        [HttpGet]
        public IActionResult AddCategory()
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
        public async Task<IActionResult> AddCategory(CreateCategoryDto createCategoryDto)
        {

            createCategoryDto.Count = 0;
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
            var responseMessage = await client.PostAsync("https://localhost:7237/api/Category/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var url = "/Admin/Category/Index/";
                return Redirect(url);
            }
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
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


            var request = await client.GetAsync("https://localhost:7237/api/Category/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var Category = JsonConvert.DeserializeObject<GetByIdCategoryDto>(responseMessage);
                return View(Category);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(GetByIdCategoryDto Category)
        {

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(Category);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
            var responseMessage = await client.PutAsync("https://localhost:7237/api/Category/" + Category.ID, stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var url = "/Admin/Category/Index/";
                return Redirect(url);
            }
            return View();

        }



        public async Task<IActionResult> DeleteCategory(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));
            var responseMessage = await client.DeleteAsync($"https://localhost:7237/api/Category/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var url2 = "/Admin/Category/Index/";
                return Redirect(url2);
            }
            var url = "/Admin/Category/Index/";
            return Redirect(url);
        }

    }
}
