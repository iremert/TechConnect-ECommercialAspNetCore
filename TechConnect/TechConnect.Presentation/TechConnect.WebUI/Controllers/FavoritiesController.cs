using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.WireProtocol.Messages;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechConnect.DtoUI.FavouriteDtos;
using TechConnect.DtoUI.ProductDto;

namespace TechConnect.WebUI.Controllers
{
    [Route("favoriler")]
    public class FavoritiesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _contextAccessor;

        public FavoritiesController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _contextAccessor = httpContextAccessor;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Favourite/GetAllFavouriteWithProductByUserID");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ResultFavouriteDto>>(responseMessage);
                ViewBag.count = products.Count();
                return View(products);
            }
            return View();
        }


        [Route("favori-kaldır/{id}")]
        public async Task<IActionResult> DeleteFavourite(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var responseMessage = await client.DeleteAsync("https://localhost:7237/api/Favourite/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("", "favoriler");
            }
            return View();


        }

        [Route("favori-ekle/{id}")]
        public async Task<IActionResult> AddFavourite(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            CreateFavouriteDto create = new CreateFavouriteDto();
      

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
            var responseMessage = await client.PostAsync("https://localhost:7237/api/Favourite/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("", "favoriler");
            }
            return View();
        }
    }
}
