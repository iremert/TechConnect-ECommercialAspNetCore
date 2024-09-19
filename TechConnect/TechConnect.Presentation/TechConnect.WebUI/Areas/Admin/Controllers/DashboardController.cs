using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using TechConnect.DtoUI.BasketTotalDtos;
using TechConnect.DtoUI.CategoryDtos;
using TechConnect.DtoUI.CommentDtos;
using TechConnect.DtoUI.CompareDtos;
using TechConnect.DtoUI.ContactDtos;
using TechConnect.DtoUI.FavouriteDtos;
using TechConnect.DtoUI.OrderingDtos;
using TechConnect.DtoUI.ProductDto;
using TechConnect.EL.Concrete;
using TechConnect.WebUI.Areas.Admin.Models;

namespace TechConnect.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
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


            var request = await client.GetAsync("https://localhost:7237/api/Favourite");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var favori = JsonConvert.DeserializeObject<List<ResultFavouriteDto>>(responseMessage);
                ViewBag.favori = favori.Count();
            }


            var request2 = await client.GetAsync("https://localhost:7237/api/Compare");
            if (request2.IsSuccessStatusCode)
            {
                var responseMessage = await request2.Content.ReadAsStringAsync();
                var compare = JsonConvert.DeserializeObject<List<ResultCompareDto>>(responseMessage);
                ViewBag.compare = compare.Count();
            }


            var request3 = await client.GetAsync("https://localhost:7237/api/Ordering");
            if (request3.IsSuccessStatusCode)
            {
                var responseMessage = await request3.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<List<ResultOrderingDto>>(responseMessage);
                ViewBag.order = order.Count();
            }


            var request4 = await client.GetAsync("https://localhost:7237/api/Category");
            if (request4.IsSuccessStatusCode)
            {
                var responseMessage = await request4.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(responseMessage);
                ViewBag.category = category.Count();
            }



            var request5 = await client.GetAsync("https://localhost:7237/api/Contact");
            if (request5.IsSuccessStatusCode)
            {
                var responseMessage = await request5.Content.ReadAsStringAsync();
                var contact = JsonConvert.DeserializeObject<List<ResultContactDto>>(responseMessage);
                ViewBag.contact = contact.Count();
            }

            var request6 = await client.GetAsync("https://localhost:5001/api/GetUsersReal/");
            if (request6.IsSuccessStatusCode)
            {
                var responseMessage = await request6.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<_GetUserRealValue2>>(responseMessage);
                ViewBag.users = users.Count();
            }


            var request7 = await client.GetAsync("https://localhost:7237/api/Product");
            if (request7.IsSuccessStatusCode)
            {
                var responseMessage = await request7.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
                ViewBag.product = product.Count();
            }


            decimal totalprice = 0;
            var request8 = await client.GetAsync("https://localhost:7237/api/Ordering");
            if (request8.IsSuccessStatusCode)
            {
                var responseMessage = await request8.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<List<ResultOrderingDto>>(responseMessage);
                foreach (var item in order)
                {
                    var request11 = await client.GetAsync("https://localhost:7237/api/BasketTotal/" + item.BasketTotalId);
                    if (request11.IsSuccessStatusCode)
                    {
                        var responseMessage1 = await request11.Content.ReadAsStringAsync();
                        var basket = JsonConvert.DeserializeObject<BasketTotal>(responseMessage1);
                        item.BasketTotal = basket;
                        totalprice = totalprice + (decimal)item.BasketTotal.TotalPrice;
                        ViewBag.price = totalprice;

                    }
                }

            }






            var request9 = await client.GetAsync("https://localhost:7237/api/Ordering");
            if (request9.IsSuccessStatusCode)
            {
                var responseMessage = await request9.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<List<ResultOrderingDto>>(responseMessage);
                var kargolandı = order.Where(x => x.OrderState.ToString() == "Kargolandı");
                ViewBag.kargolandı = kargolandı.Count();

                var tamamlandı = order.Where(x => x.OrderState.ToString() == "Tamamlandı");
                ViewBag.tamamlandı = tamamlandı.Count();

                var paketlendi = order.Where(x => x.OrderState.ToString() == "Paketlendi");
                ViewBag.paketlendi = paketlendi.Count();

                var bekleniyor = order.Where(x => x.OrderState.ToString() == "Bekleniyor");
                ViewBag.bekleniyor = bekleniyor.Count();
            }






            var request12 = await client.GetAsync("https://localhost:7237/api/Ordering");
            if (request12.IsSuccessStatusCode)
            {
                var responseMessage = await request12.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<List<ResultOrderingDto>>(responseMessage);
              
                if(order!=null)
                {
                    foreach (var item in order)
                    {
                        var request122 = await client.GetAsync("https://localhost:7237/api/BasketTotal/" + item.BasketTotalId);
                        if (request122.IsSuccessStatusCode)
                        {
                            var responseMessage1 = await request122.Content.ReadAsStringAsync();
                            var basket = JsonConvert.DeserializeObject<BasketTotal>(responseMessage1);
                            
                            item.BasketTotal = basket;
                            totalprice = totalprice + (decimal)item.BasketTotal.TotalPrice;
                            ViewBag.price = totalprice;

                        }
                    }
                }
                else
                {
                    ViewBag.price = 0;
                }
                
                
                var startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek); // Haftanın başlangıcı
                var endOfWeek = startOfWeek.AddDays(7); // Haftanın sonu

                var order2 = order
                    .Where(x => x.OrderDeliveryDate >= startOfWeek && x.OrderDeliveryDate < endOfWeek);
                decimal price2 = 0;

                if(order2!=null)
                {
                    foreach (var item in order2)
                    {
                        price2 = price2 + (decimal)item.BasketTotal.TotalPrice;
                    }
                    ViewBag.week = price2;
                }
                else
                {
                    ViewBag.week = 0;
                }
                
            }






            var request13 = await client.GetAsync("https://localhost:7237/api/Ordering");
            if (request13.IsSuccessStatusCode)
            {
                var responseMessage = await request13.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<List<ResultOrderingDto>>(responseMessage);
                var pricee = order.Where(x => x.OrderDeliveryDate.Month == DateTime.Today.Month && x.OrderDeliveryDate.Year==DateTime.Today.Year && x.OrderDeliveryDate.Day == DateTime.Today.Day).SingleOrDefault();
                if(pricee!=null)
                {
                    var request11 = await client.GetAsync("https://localhost:7237/api/BasketTotal/" + pricee.BasketTotalId);
                    if (request11.IsSuccessStatusCode)
                    {
                        var responseMessage1 = await request11.Content.ReadAsStringAsync();
                        var basket = JsonConvert.DeserializeObject<BasketTotal>(responseMessage1);
                        pricee.BasketTotal = basket;

                    }
                    var price3 = pricee.BasketTotal.TotalPrice;
                    ViewBag.today = price3;
                }
                else
                {
                    ViewBag.today = 0;
                }
                
            }
            return View();
        }
    }
}
