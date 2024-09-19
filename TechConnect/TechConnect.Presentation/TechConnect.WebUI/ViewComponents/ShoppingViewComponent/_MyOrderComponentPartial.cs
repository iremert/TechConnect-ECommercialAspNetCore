using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.ContentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TechConnect.DtoUI.BasketTotalDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.WebUI.ViewComponents.ShoppingViewComponent
{
    public class _MyOrderComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _MyOrderComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));



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



            var request = await client.GetAsync("https://localhost:7237/api/BasketTotal/");
            var responseMessage2 = await request.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<GetByIdBasketTotalDto>>(responseMessage2);
            var deger = values.Find(x => x.UserId == userIdClaim.Value && x.IsFinished == false);
            if (deger!=null)
            {
                var request2 = await client.GetAsync("https://localhost:7237/api/Discount/" + deger.DiscountId);
                var discount = 0;
                if(request2.IsSuccessStatusCode)
                {
                    var responseMessage3 = await request2.Content.ReadAsStringAsync();
                    discount = JsonConvert.DeserializeObject<Discount>(responseMessage3).Rate;
                }
                else
                {
                    discount = 0;
                }
                
                var deger2 = 0;

                foreach (var item in deger.BasketItems)
                {
                    var request3 = await client.GetAsync("https://localhost:7237/api/Product/" + item.ProductId);
                    var responseMessage4 = await request3.Content.ReadAsStringAsync();
                    var product3 = JsonConvert.DeserializeObject<Product>(responseMessage4);

                    item.Product=product3;
                    
                }
                ViewBag.totalpricee = deger.TotalPrice - ((deger.TotalPrice * discount) / 100) + 100;
                return View(deger);
            }
            else
            {
                return View();
            }

            
        }
    }
}
