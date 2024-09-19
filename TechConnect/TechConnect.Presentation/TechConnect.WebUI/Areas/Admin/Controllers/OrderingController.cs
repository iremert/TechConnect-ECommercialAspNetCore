using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TechConnect.DtoUI.OrderingDtos;
using TechConnect.WebUI.Areas.Admin.Models;

namespace TechConnect.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderingController(IHttpClientFactory httpClientFactory)
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


            var request = await client.GetAsync("https://localhost:7237/api/Ordering");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var Orderings = JsonConvert.DeserializeObject<List<ResultOrderingDto>>(responseMessage);

                foreach(var item in Orderings)
                {
                    var responseMessage2 = await client.GetAsync("https://localhost:5001/api/GetUserReal/" + item.UserId);
                    if (responseMessage2 != null)
                    {
                        var jsondata = await responseMessage2.Content.ReadAsStringAsync();
                        var uservalues = JsonConvert.DeserializeObject<_GetUserRealValue2>(jsondata);
                        //yeni bir ordering sınfı yapıp atama yap userına yada name surname email alanı ekle daha iyi
                    }
                    return View(Orderings);
                }
               
            }
            return View();
        }






        public async Task<IActionResult> DeleteOrdering(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));
            var responseMessage = await client.DeleteAsync($"https://localhost:7237/api/Ordering/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var url2 = "/Admin/Ordering/Index/";
                return Redirect(url2);
            }
            var url = "/Admin/Ordering/Index/";
            return Redirect(url);
        }



    }
}
