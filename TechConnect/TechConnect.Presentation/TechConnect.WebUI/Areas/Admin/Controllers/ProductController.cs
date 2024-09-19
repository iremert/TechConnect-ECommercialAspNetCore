using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TechConnect.DtoUI.CategoryDtos;
using TechConnect.DtoUI.ColorDtos;
using TechConnect.DtoUI.ProductDto;
using TechConnect.EL.Concrete;

namespace TechConnect.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
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


            var request = await client.GetAsync("https://localhost:7237/api/Product");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
                return View(products);
            }
            return View();
        }





        [HttpGet]
        public async Task<IActionResult> AddProduct()
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

            var request = await client.GetAsync("https://localhost:7237/api/Category");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(responseMessage);
                List<SelectListItem> degerler = (from x in category
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.ID.ToString(),
                                                 }).ToList();
                ViewBag.degerler = degerler;
            }

            var request2 = await client.GetAsync("https://localhost:7237/api/Color");
            if (request2.IsSuccessStatusCode)
            {
                var responseMessage = await request2.Content.ReadAsStringAsync();
                var color = JsonConvert.DeserializeObject<List<ResultColorDto>>(responseMessage);
                List<SelectListItem> degerler = (from x in color
                                                 select new SelectListItem
                                                 {
                                                     Text = x.ColorName,
                                                     Value = x.ID.ToString(),
                                                 }).ToList();
                ViewBag.degerler2 = degerler;
            }



            CreateProductDto createProductDto = new CreateProductDto();
            // Teknik özellikleri ekleme
            createProductDto.TechnicalSpecifications["Processor"] = "Değer giriniz..";
            createProductDto.TechnicalSpecifications["RAM"] = "Değer giriniz..";
            createProductDto.TechnicalSpecifications["Storage"] = "Değer giriniz..";
            createProductDto.TechnicalSpecifications["Display"] = "Değer giriniz..";
            createProductDto.TechnicalSpecifications["Graphics"] = "Değer giriniz..";
            createProductDto.TechnicalSpecifications["Operating System"] = "Değer giriniz..";
            createProductDto.TechnicalSpecifications["Battery Life"] = "Değer giriniz..";
            createProductDto.TechnicalSpecifications["Weight"] = "Değer giriniz..";
            createProductDto.TechnicalSpecifications["Ports"] = "Değer giriniz..";
            createProductDto.TechnicalSpecifications["Size"] = "Değer giriniz..";
            return View(createProductDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto createProductDto)
        {

            foreach (var spec in createProductDto.TechnicalSpecifications)
            {
                // Teknik özellikleri ekleme
                createProductDto.TechnicalSpecifications[spec.Key] = spec.Value;
            }
            



            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
            var responseMessage = await client.PostAsync("https://localhost:7237/api/Product/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var url = "/Admin/Product/Index/";
                return Redirect(url);
            }
            return View();
        }





        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
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




            var request = await client.GetAsync("https://localhost:7237/api/Category");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(responseMessage);
                List<SelectListItem> degerler = (from x in category
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.ID.ToString(),
                                                 }).ToList();
                ViewBag.degerler = degerler;
            }

            var request2 = await client.GetAsync("https://localhost:7237/api/Color");
            if (request2.IsSuccessStatusCode)
            {
                var responseMessage = await request2.Content.ReadAsStringAsync();
                var color = JsonConvert.DeserializeObject<List<ResultColorDto>>(responseMessage);
                List<SelectListItem> degerler = (from x in color
                                                 select new SelectListItem
                                                 {
                                                     Text = x.ColorName,
                                                     Value = x.ID.ToString(),
                                                 }).ToList();
                ViewBag.degerler2 = degerler;
            }







            var request3 = await client.GetAsync("https://localhost:7237/api/Product/" + id);
            if (request3.IsSuccessStatusCode)
            {
                var responseMessage = await request3.Content.ReadAsStringAsync();
                var Product = JsonConvert.DeserializeObject<GetByIdProductDto>(responseMessage);
                return View(Product);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(GetByIdProductDto Product)
        {
            foreach (var spec in Product.TechnicalSpecifications)
            {
                // Teknik özellikleri ekleme
                Product.TechnicalSpecifications[spec.Key] = spec.Value;
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(Product);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
            var responseMessage = await client.PutAsync("https://localhost:7237/api/Product/" + Product.ID, stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var url = "/Admin/Product/Index/";
                return Redirect(url);
            }
            return View();

        }



        public async Task<IActionResult> DeleteProduct(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));
            var responseMessage = await client.DeleteAsync($"https://localhost:7237/api/Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var url2 = "/Admin/Product/Index/";
                return Redirect(url2);
            }
            var url = "/Admin/Product/Index/";
            return Redirect(url);
        }



        public async Task<IActionResult> DoActiveAvailable(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Product/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage2 = await request.Content.ReadAsStringAsync();
                var Product = JsonConvert.DeserializeObject<GetByIdProductDto>(responseMessage2);
                Product.IsAvailable = true;
                var jsonData = JsonConvert.SerializeObject(Product);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var responseMessage = await client.PutAsync("https://localhost:7237/api/Product/" + id, stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var url2 = "/Admin/Product/Index/";
                    return Redirect(url2);
                }
            }




            var url = "/Admin/Product/Index/";
            return Redirect(url);
        }

        public async Task<IActionResult> DoPassiveAvailable(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Product/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage2 = await request.Content.ReadAsStringAsync();
                var Product = JsonConvert.DeserializeObject<GetByIdProductDto>(responseMessage2);
                Product.IsAvailable = false;
                var jsonData = JsonConvert.SerializeObject(Product);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var responseMessage = await client.PutAsync("https://localhost:7237/api/Product/" + id, stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var url2 = "/Admin/Product/Index/";
                    return Redirect(url2);
                }
            }




            var url = "/Admin/Product/Index/";
            return Redirect(url);
        }







        public async Task<IActionResult> DoActiveFeatured(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Product/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage2 = await request.Content.ReadAsStringAsync();
                var Product = JsonConvert.DeserializeObject<GetByIdProductDto>(responseMessage2);
                Product.IsFeatured = true;
                var jsonData = JsonConvert.SerializeObject(Product);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var responseMessage = await client.PutAsync("https://localhost:7237/api/Product/" + id, stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var url2 = "/Admin/Product/Index/";
                    return Redirect(url2);
                }
            }




            var url = "/Admin/Product/Index/";
            return Redirect(url);
        }

        public async Task<IActionResult> DoPassiveFeatured(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Product/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage2 = await request.Content.ReadAsStringAsync();
                var Product = JsonConvert.DeserializeObject<GetByIdProductDto>(responseMessage2);
                Product.IsFeatured = false;
                var jsonData = JsonConvert.SerializeObject(Product);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var responseMessage = await client.PutAsync("https://localhost:7237/api/Product/" + id, stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var url2 = "/Admin/Product/Index/";
                    return Redirect(url2);
                }
            }




            var url = "/Admin/Product/Index/";
            return Redirect(url);
        }
    }
}
