using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechConnect.DtoUI.CompareDtos;
using TechConnect.DtoUI.ProductDto;

namespace TechConnect.WebUI.Controllers
{
    [Route("karşılaştır")]
    public class CompareController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CompareController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Compare/GetAllCompareWithProductByUserID");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ResultCompareDto>>(responseMessage);
                ViewBag.count = products.Count();
                return View(products);
            }
            return View();
        }



        [Route("karşılaştır-kaldır/{id}")]
        public async Task<IActionResult> DeleteCompare(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var responseMessage = await client.DeleteAsync("https://localhost:7237/api/Compare/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("", "karşılaştır");
            }
            return View();
        }

        [Route("karşılaştır-ekle/{id}")]
        public async Task<IActionResult> AddCompare(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));



            CreateCompareDto create = new CreateCompareDto();


            var token = HttpContext.Session.GetString("AuthToken");
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null)
            {
                return View();
            }
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim.Value))
            {
                return View();
            }
            create.ProductID = id;
            create.UserID = userIdClaim.Value;
            var jsonData = JsonConvert.SerializeObject(create);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7237/api/Compare/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("", "karşılaştır");
            }
            return View();

        }
    }
}
