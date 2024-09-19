using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.ContentModel;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechConnect.DtoUI.AddressDtos;
using TechConnect.DtoUI.BasketTotalDtos;
using TechConnect.DtoUI.DiscountDtos;
using TechConnect.DtoUI.ProductDto;
using TechConnect.EL.Concrete;
using TechConnect.WebUI.Services.Concrete;

namespace TechConnect.WebUI.Controllers
{
    [Route("sipariş-sepeti")]
    public class ShoppingCartController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ShoppingCartController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("")]
        public async Task<IActionResult> Index(string discountid)
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token2 = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token2);
            }
            var token3 = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken3 = tokenHandler.ReadJwtToken(token3);
            // Payload kısmına erişim
            var claims = jwtToken3.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }





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
            var values = JsonConvert.DeserializeObject<List<ResultBasketTotalDto>>(responseMessage2);
            if (values.Find(x => x.UserId == userIdClaim.Value && x.IsFinished == false && x.BasketItems.Count() > 0) != null)
            {
                var basket = values.Where(x => x.UserId == userIdClaim.Value && x.IsFinished == false).SingleOrDefault();

                if(discountid!=null)
                {
                    var request11 = await client.GetAsync("https://localhost:7237/api/Discount/" + discountid);
                    var responseMessage11 = await request11.Content.ReadAsStringAsync();
                    var values11 = JsonConvert.DeserializeObject<Discount>(responseMessage11);


                    var discountt = values11;
                    basket.DiscountId = discountt.ID;
                    basket.discount = discountt;
                }
              
                else
                {
                    if (basket.DiscountId != null && basket.DiscountId != "irem")
                    {
                        var request11 = await client.GetAsync("https://localhost:7237/api/Discount/" + basket.DiscountId);
                        var responseMessage11 = await request11.Content.ReadAsStringAsync();
                        var values11 = JsonConvert.DeserializeObject<Discount>(responseMessage11);


                        var discountt = values11;
                        basket.DiscountId = discountt.ID;
                        basket.discount = discountt;

                    }
                    else
                    {
                        var discountt = new Discount()
                        {
                            Code = "irem",
                            ID = "irem",
                            IsActive = false,
                            Rate = 0,
                            ValidDate = DateTime.Now
                        };
                        basket.discount = discountt;
                    }
                    
                }

                foreach (var item in basket.BasketItems)
                {
                    var request3 = await client.GetAsync("https://localhost:7237/api/Product/" + item.ProductId);
                    var responseMessage4 = await request3.Content.ReadAsStringAsync();
                    var product3 = JsonConvert.DeserializeObject<Product>(responseMessage4);

                    var request7 = await client.GetAsync("https://localhost:7237/api/Color/" + product3.ColorId);
                    var responseMessage7 = await request7.Content.ReadAsStringAsync();
                    var color7 = JsonConvert.DeserializeObject<EL.Concrete.Color>(responseMessage7);

                    var request8 = await client.GetAsync("https://localhost:7237/api/Category/" + product3.CategoryId);
                    var responseMessage8 = await request8.Content.ReadAsStringAsync();
                    var category8 = JsonConvert.DeserializeObject<Category>(responseMessage8);

                    item.Product.Category = category8;
                    item.Product.Color = color7;
                }
                var jsondata = JsonConvert.SerializeObject(basket);
                StringContent stringcontent = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var responseMessage3 = await client.PutAsync("https://localhost:7237/api/BasketTotal/" + basket.ID, stringcontent);
                ViewBag.discountrate = basket.discount.Rate;
                ViewBag.totalprice = basket.TotalPrice - ((basket.TotalPrice * basket.discount.Rate) / 100) + 100;
                return View(basket);
            }


            else
            {
                return RedirectToAction("sepet-boş", "sipariş-sepeti");

            }
        }


        [Route("sepet-boş")]
        public async Task<IActionResult> Index4()
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token2 = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token2);
            }
            var token3 = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken3 = tokenHandler.ReadJwtToken(token3);
            // Payload kısmına erişim
            var claims = jwtToken3.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }



            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string tokenn = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", tokenn);
            }

            return View();
        }



        [Route("adres-işlemleri")]
        public async Task<IActionResult> Index2()
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token2 = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token2);
            }
            var token3 = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken3 = tokenHandler.ReadJwtToken(token3);
            // Payload kısmına erişim
            var claims = jwtToken3.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }


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



            var request = await client.GetAsync("https://localhost:7237/api/Address/");
            var responseMessage2 = await request.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultAddressDto>>(responseMessage2);
            if (values.Find(x => x.UserId == userIdClaim.Value) != null)
            {
                var abouts = values.FindAll(x => x.UserId == userIdClaim.Value).ToList();
                return View(abouts);
            }
            else
            {
                return RedirectToAction("adres-işlemleri-adres-yok", "sipariş-sepeti");
            }

        }

        [Route("adres-işlemleri-adres-yok")]
        public async Task<IActionResult> Index5()
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token2 = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token2);
            }
            var token3 = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken3 = tokenHandler.ReadJwtToken(token3);
            // Payload kısmına erişim
            var claims = jwtToken3.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }
            return View();
        }





        [Route("güvenli-ödeme/{id}")]
        public async Task<IActionResult> Index3(string ID)
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token2 = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token2);
            }
            var token3 = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken3 = tokenHandler.ReadJwtToken(token3);
            // Payload kısmına erişim
            var claims = jwtToken3.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }
            ViewBag.addressid = ID;
            return View();
        }





        [Route("sepete-ekle/{id}")]
        public async Task<IActionResult> AddBasketItem(string id)
        {
            var token3 = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken3 = tokenHandler.ReadJwtToken(token3);
            // Payload kısmına erişim
            var claims = jwtToken3.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            //çalışıyor
            var request = await client.GetAsync("https://localhost:7237/api/Product/" + id);
            var responseMessage = await request.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(responseMessage);

            var request5 = await client.GetAsync("https://localhost:7237/api/Color/" + product.ColorId);
            var responseMessage5 = await request5.Content.ReadAsStringAsync();
            var color = JsonConvert.DeserializeObject<EL.Concrete.Color>(responseMessage5);

            var request6 = await client.GetAsync("https://localhost:7237/api/Category/" + product.CategoryId);
            var responseMessage6 = await request6.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<Category>(responseMessage6);



            var basketitem = new BasketItem();
            product.Category = category;
            product.CategoryId = category.ID;
            product.Color = color;
            product.ColorId = color.ID;
            basketitem.Product = product;
            basketitem.ProductId = product.ID;
            basketitem.Quantity = 1;



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

            var request2 = await client.GetAsync("https://localhost:7237/api/BasketTotal/");
            var responseMessage2 = await request2.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<UpdateBasketTotalDto>>(responseMessage2);
            if (values.Find(x => x.UserId == userIdClaim.Value && x.IsFinished == false) != null)
            {
                var basket = values.Where(x => x.UserId == userIdClaim.Value && x.IsFinished == false).SingleOrDefault();
                var discountt = new Discount()
                {
                    Code = "irem",
                    ID = "irem",
                    IsActive = false,
                    Rate = 0,
                    ValidDate = DateTime.Now
                };
                basket.discount = discountt;
                foreach (var item in basket.BasketItems)
                {
                    var request3 = await client.GetAsync("https://localhost:7237/api/Product/" + item.ProductId);
                    var responseMessage4 = await request3.Content.ReadAsStringAsync();
                    var product3 = JsonConvert.DeserializeObject<Product>(responseMessage4);

                    var request7 = await client.GetAsync("https://localhost:7237/api/Color/" + product3.ColorId);
                    var responseMessage7 = await request7.Content.ReadAsStringAsync();
                    var color7 = JsonConvert.DeserializeObject<EL.Concrete.Color>(responseMessage7);

                    var request8 = await client.GetAsync("https://localhost:7237/api/Category/" + product3.CategoryId);
                    var responseMessage8 = await request8.Content.ReadAsStringAsync();
                    var category8 = JsonConvert.DeserializeObject<Category>(responseMessage8);

                    item.Product.Category = category8;
                    item.Product.Color = color7;
                }

                if (basket.BasketItems.Where(x => x.ProductId == id).SingleOrDefault() != default)
                {
                    var deger = basket.BasketItems.Where(x => x.ProductId == id).SingleOrDefault();
                    deger.Quantity = deger.Quantity + 1;
                    basket.TotalPrice = (decimal)basket.BasketItems.Sum(x => x.Product.ProductPrice * x.Quantity);
                    var jsondata = JsonConvert.SerializeObject(basket);
                    StringContent stringcontent = new StringContent(jsondata, Encoding.UTF8, "application/json");
                    var responseMessage3 = await client.PutAsync("https://localhost:7237/api/BasketTotal/" + basket.ID, stringcontent);
                    if (responseMessage3.IsSuccessStatusCode)
                    {
                        return RedirectToAction("", "sipariş-sepeti");
                    }
                    return RedirectToAction("", "ürünler");
                }
                else
                {
                    basket.BasketItems.Add(basketitem);
                    basket.TotalPrice = (decimal)basket.BasketItems.Sum(x => x.Product.ProductPrice * x.Quantity);

                    var jsondata = JsonConvert.SerializeObject(basket);
                    StringContent stringcontent = new StringContent(jsondata, Encoding.UTF8, "application/json");
                    var responseMessage3 = await client.PutAsync("https://localhost:7237/api/BasketTotal/" + basket.ID, stringcontent);
                    if (responseMessage3.IsSuccessStatusCode)
                    {
                        return RedirectToAction("", "sipariş-sepeti");
                    }
                    return RedirectToAction("", "ürünler");
                }

            }


            else
            {
                //çalışıyor

                var createBasketTotal = new CreateBasketTotalDto();
                createBasketTotal.IsFinished = false;
                createBasketTotal.UserId = userIdClaim.Value;
                createBasketTotal.BasketItems = new List<BasketItem>();
                createBasketTotal.BasketItems.Add(basketitem);


                var discountt = new Discount()
                {
                    Code = "irem",
                    ID = "irem",
                    IsActive = false,
                    Rate = 0,
                    ValidDate = DateTime.Now
                };

                createBasketTotal.discount = discountt;
                createBasketTotal.DiscountId = discountt.ID;
                createBasketTotal.TotalPrice = (decimal)createBasketTotal.BasketItems.Sum(x => x.Product.ProductPrice * x.Quantity);


                var jsonData = JsonConvert.SerializeObject(createBasketTotal);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var response = await client.PostAsync("https://localhost:7237/api/BasketTotal/", stringContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("", "sipariş-sepeti");
                }
                else
                {
                    return RedirectToAction("", "ürünler");
                }

            }
        }



        [Route("sepetten-çıkar/{id}")]
        public async Task<IActionResult> RemoveBasketItem(string id)
        {
            var token3 = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken3 = tokenHandler.ReadJwtToken(token3);
            // Payload kısmına erişim
            var claims = jwtToken3.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }


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


            var request2 = await client.GetAsync("https://localhost:7237/api/BasketTotal/");
            var responseMessage2 = await request2.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBasketTotalDto>>(responseMessage2);

            var basket = values.Where(x => x.UserId == userIdClaim.Value && x.IsFinished == false).SingleOrDefault();
            var discountt = new Discount()
            {
                Code = "irem",
                ID = "irem",
                IsActive = false,
                Rate = 0,
                ValidDate = DateTime.Now
            };
            basket.discount = discountt;
            foreach (var item in basket.BasketItems)
            {
                var request3 = await client.GetAsync("https://localhost:7237/api/Product/" + item.ProductId);
                var responseMessage4 = await request3.Content.ReadAsStringAsync();
                var product3 = JsonConvert.DeserializeObject<Product>(responseMessage4);

                var request7 = await client.GetAsync("https://localhost:7237/api/Color/" + product3.ColorId);
                var responseMessage7 = await request7.Content.ReadAsStringAsync();
                var color7 = JsonConvert.DeserializeObject<EL.Concrete.Color>(responseMessage7);

                var request8 = await client.GetAsync("https://localhost:7237/api/Category/" + product3.CategoryId);
                var responseMessage8 = await request8.Content.ReadAsStringAsync();
                var category8 = JsonConvert.DeserializeObject<Category>(responseMessage8);

                item.Product.Category = category8;
                item.Product.Color = color7;
            }


            var value = basket.BasketItems.Where(x => x.ProductId == id).SingleOrDefault();
            basket.BasketItems.Remove(value);
            basket.TotalPrice = (decimal)basket.BasketItems.Sum(x => x.Product.ProductPrice * x.Quantity);

            var jsondata = JsonConvert.SerializeObject(basket);
            StringContent stringcontent = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var responseMessage3 = await client.PutAsync("https://localhost:7237/api/BasketTotal/" + basket.ID, stringcontent);
            if (responseMessage3.IsSuccessStatusCode)
            {
                return RedirectToAction("", "sipariş-sepeti");
            }
            return RedirectToAction("", "sipariş-sepeti");
        }
    }
}
