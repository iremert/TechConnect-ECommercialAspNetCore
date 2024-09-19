
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechConnect.DtoUI.BasketTotalDtos;
using TechConnect.DtoUI.IdentityDtos.LoginDtos;
using TechConnect.DtoUI.OrderingDtos;
using TechConnect.EL.Concrete;
using TechConnect.IdentityServer.Models;
using TechConnect.WebUI.Models;
using TechConnect.WebUI.Services.Concrete;

namespace TechConnect.WebUI.Controllers
{
    [Route("iyzico")]
    public class İyzicoPaymentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
      
        public İyzicoPaymentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //[Route("")]
        //public async Task<IActionResult> Index()

        //{

        //    var client = _httpClientFactory.CreateClient();
        //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

        //    var request = await client.GetAsync("https://localhost:7237/api/İyzicoPayment/Pay/");
        //    if (request.IsSuccessStatusCode)
        //    {
        //        var responseMessage = await request.Content.ReadAsStringAsync();

        //        // JSON içerisindeki HTML content'i ayıklayalım
        //        dynamic jsonResponse = JsonConvert.DeserializeObject(responseMessage);
        //        string htmlContent = jsonResponse["content"]; // HTML içeriği burada
        //        return Content(htmlContent, "text/html");
        //    }
        //    return View();
        //}


        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index(CreditAndUser creditAndUser)
        {
            creditAndUser.PhoneNumber = "+905350000000";
            var valuesss = creditAndUser;
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
            creditAndUser.Price=(basket.TotalPrice - ((basket.TotalPrice * basket.discount.Rate) / 100) + 100).ToString();






            var jsonData = JsonConvert.SerializeObject(creditAndUser);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var request = await client.PostAsync("https://localhost:7237/api/İyzicoPayment/Pay", stringContent);
            if (request.IsSuccessStatusCode)
            {



                var responseMessage = await request.Content.ReadAsStringAsync();

                // JSON içerisindeki HTML content'i ayıklayalım
                dynamic jsonResponse = JsonConvert.DeserializeObject(responseMessage);
                string htmlContent = jsonResponse["content"]; // HTML içeriği burada
                //TempData["HtmlContent"] = htmlContent;
                return Content(htmlContent, "text/html");
            }
            return View();
        }


        [Route("3D-secure")]
        public async Task< IActionResult> Index2()
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

            return View();
        }



        [Route("3D-secure")]
        [HttpPost]
        public async Task<IActionResult> Index2(CreditCartInformation creditCartInformation)
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




            if (creditCartInformation.ExpirationDateYear==null || creditCartInformation.ExpirationDateMonth==null)
            {
                return RedirectToAction("güvenli-ödeme", "sipariş-sepeti", new {ID=creditCartInformation.AddressId});
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


            var uservalues =new UserForİyzico();
            var responseMessage = await client.GetAsync("https://localhost:5001/api/GetUser/"+ userIdClaim.Value);
            if (responseMessage != null)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                uservalues = JsonConvert.DeserializeObject<UserForİyzico>(jsondata);
                if (uservalues == null)
                { return View(); }
            }
            else
            {
                return View();
            }


            CreditAndUser creditAndUser = new CreditAndUser();
            creditAndUser.CVV=creditCartInformation.CVV;
            creditAndUser.ExpirationDateMonth=creditCartInformation.ExpirationDateMonth;
            creditAndUser.ExpirationDateYear=creditCartInformation.ExpirationDateYear;
            creditAndUser.CartNumber=creditCartInformation.CartNumber;
            creditAndUser.NameSurname=creditCartInformation.NameSurname;
            creditAndUser.Name=uservalues.Name;
            creditAndUser.Surname=uservalues.Surname;
            creditAndUser.PhoneNumber= uservalues.PhoneNumber;
            creditAndUser.Email=uservalues.Email;
            creditAndUser.AddressId = creditCartInformation.AddressId;
            creditAndUser.Price = 0.ToString();

            return View(creditAndUser);
        }


        [Route("order-ekle")]
        [HttpPost]
        public async Task<IActionResult> AddOrder(string addressid)
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
            var values = JsonConvert.DeserializeObject<List<ResultBasketTotalDto>>(responseMessage2);
            var basket = values.Where(x => x.UserId == userIdClaim.Value && x.IsFinished == false).SingleOrDefault();
            basket.IsFinished = true;
            if (basket.DiscountId != null && basket.DiscountId!="irem")
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



            CreateOrderingDto createOrderingDto = new CreateOrderingDto();
            createOrderingDto.AddressId=addressid;
            createOrderingDto.UserId = userIdClaim.Value;
            createOrderingDto.BasketTotalId = basket.ID;
            createOrderingDto.OrderDate=DateTime.Now;
            createOrderingDto.OrderDeliveryDate=DateTime.Now;
            createOrderingDto.OrderState = OrderState.Bekleniyor;

            var jsonData = JsonConvert.SerializeObject(createOrderingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
            var responseMessage = await client.PostAsync("https://localhost:7237/api/Ordering/", stringContent);
            return View(); 
        }
    }
}
