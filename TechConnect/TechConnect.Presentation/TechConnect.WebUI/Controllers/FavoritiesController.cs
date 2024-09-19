using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.WireProtocol.Messages;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechConnect.DtoUI.FavouriteDtos;
using TechConnect.DtoUI.ProductDto;
using TechConnect.WebUI.Services.Concrete;

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

        //private JwtSecurityToken DecodeToken(string token)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    return handler.ReadJwtToken(token);
        //}

        //private bool UserHasScope(string token, string requiredScope)
        //{
        //    var jwtToken = DecodeToken(token);
        //    var scopes = jwtToken.Claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
        //    return scopes.Contains(requiredScope);
        //}

        [Route("")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token2 = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token2);
            }
            var token = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            // Payload kısmına erişim
            var claims = jwtToken.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if(!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }

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
            var token = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            // Payload kısmına erişim
            var claims = jwtToken.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }


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

            var token2 = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken2 = tokenHandler.ReadJwtToken(token2);
            // Payload kısmına erişim
            var claims = jwtToken2.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }

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
