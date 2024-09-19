using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using TechConnect.DtoUI.BasketTotalDtos;
using TechConnect.DtoUI.OrderingDtos;
using TechConnect.EL.Concrete;
using TechConnect.WebUI.Services.Concrete;

namespace TechConnect.WebUI.Controllers
{
    [Route("siparişlerim")]
    public class OrderingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("{id}")]
        public async Task<IActionResult> Index(string id)
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
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }




            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));



            var request = await client.GetAsync("https://localhost:7237/api/Ordering/");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultOrderingDto>>(responseMessage);
                values=values.Where(x => x.UserId == id).ToList();
               
                foreach(var item in values)
                {
                    var request2 = await client.GetAsync("https://localhost:7237/api/BasketTotal/" + item.BasketTotalId);
                    if (request2.IsSuccessStatusCode)
                    {
                        var responseMessage2 = await request2.Content.ReadAsStringAsync();
                        var values2 = JsonConvert.DeserializeObject<BasketTotal>(responseMessage2);
                        item.BasketTotal = values2;
                    }
                }
                


                return View(values);
            }

            return View();
        }




        [Route("sipariş-detaylarım/{id}")]
        public async Task<IActionResult> Index2(string id)
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
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }




            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var request = await client.GetAsync("https://localhost:7237/api/Ordering/");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultOrderingDto>>(responseMessage);
                values = values.Where(x => x.UserId == id).ToList();

                foreach (var item in values)
                {
                    var request2 = await client.GetAsync("https://localhost:7237/api/BasketTotal/" + item.BasketTotalId);
                    if (request2.IsSuccessStatusCode)
                    {
                        var responseMessage2 = await request2.Content.ReadAsStringAsync();
                        var values2 = JsonConvert.DeserializeObject<BasketTotal>(responseMessage2);
                        item.BasketTotal = values2;

                    }

                    var request3 = await client.GetAsync("https://localhost:7237/api/Address/" + item.AddressId);
                    if (request3.IsSuccessStatusCode)
                    {
                        var responseMessage2 = await request3.Content.ReadAsStringAsync();
                        var values3 = JsonConvert.DeserializeObject<Address>(responseMessage2);
                        item.Address = values3;
                    }


                }



                return View(values);
            }




            return View();
        }
    }
}
