using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TechConnect.DtoUI.BasketTotalDtos;

namespace TechConnect.WebUI.ViewComponents.LayoutViewComponent
{



    public class _ShoppingValueComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ShoppingValueComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("AuthToken");

            if (string.IsNullOrEmpty(token))
            {
                // Eğer token null ya da boş ise, oturum açılmamış demektir, bu yüzden sepet bilgilerini sıfırla ve görünümü döndür.
                ViewBag.shoppingcount = 0;
                ViewBag.shoppingprice = 0.00;
                return View();
            }

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null)
            {
                ViewBag.shoppingcount = 0;
                ViewBag.shoppingprice = 0.00;
                return View();
            }

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim?.Value))
            {
                ViewBag.shoppingcount = 0;
                ViewBag.shoppingprice = 0.00;
                return View();
            }

            var request = await client.GetAsync("https://localhost:7237/api/BasketTotal/");
            if (!request.IsSuccessStatusCode)
            {
                ViewBag.shoppingcount = 0;
                ViewBag.shoppingprice = 0.00;
                return View();
            }

            var responseMessage2 = await request.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBasketTotalDto>>(responseMessage2);

            var currentBasket = values?.FirstOrDefault(x => x.UserId == userIdClaim.Value && x.IsFinished == false);

            if (currentBasket != null)
            {
                ViewBag.shoppingcount = currentBasket.BasketItems.Select(x=>x.Quantity)?.Sum() ?? 0;
                ViewBag.shoppingprice = currentBasket.TotalPrice;
            }
            else
            {
                ViewBag.shoppingcount = 0;
                ViewBag.shoppingprice = 0.00;
            }

            return View();
        }


    }
}
