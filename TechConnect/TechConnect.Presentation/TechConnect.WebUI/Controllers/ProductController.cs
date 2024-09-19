using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TechConnect.DtoUI.CommentDtos;
using TechConnect.DtoUI.CompareDtos;
using TechConnect.DtoUI.FavouriteDtos;
using TechConnect.DtoUI.ProductDto;
using TechConnect.EL.Concrete;
using TechConnect.WebUI.Models;
using TechConnect.WebUI.Services.Concrete;

namespace TechConnect.WebUI.Controllers
{
    [Route("ürünler")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public static List<ResultProductDto> products = new List<ResultProductDto>();


        private bool UserHasScope()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier);
            if(userIdClaim==null)
            {
                return false;
            }
            return true;
           
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token);
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var deger = UserHasScope();
            if (deger == false)
            {
                var request5 = await client.GetAsync("https://localhost:7237/api/Product/");
                if (request5.IsSuccessStatusCode)
                {
                    var responseMessage = await request5.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
                    products.ForEach(x => products.Select(x => x.IsFavourite == false));
                    products.ForEach(x => products.Select(x => x.IsCompare == false));
                    ViewBag.count = products.Count();
                    foreach (var item in products)
                    {
                        var request4 = await client.GetAsync("https://localhost:7237/api/Comment/");
                        if (request4.IsSuccessStatusCode)
                        {
                            var responseMessage4 = await request4.Content.ReadAsStringAsync();
                            var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(responseMessage4);
                            var countrating = values.Where(x => x.ProductId == item.ID && x.Status == true).Select(x => x.Rating).Count();
                            var sumrating = values.Where(x => x.ProductId == item.ID && x.Status == true).Select(x => x.Rating).Sum();
                            if(countrating!=0)
                            {
                                item.Rate = (int)(sumrating / countrating);
                            }
                            else
                            {
                                item.Rate = 0;
                            }
                            
                        }
                    }

                    return View(products);
                }
            }
            var request2 = await client.GetAsync("https://localhost:7237/api/Favourite/GetAllFavouriteWithProductByUserID");
            var responseMessage2 = await request2.Content.ReadAsStringAsync();
            var favori = JsonConvert.DeserializeObject<List<ResultFavouriteDto>>(responseMessage2);


            var request3 = await client.GetAsync("https://localhost:7237/api/Compare/GetAllCompareWithProductByUserID");
            var responseMessage3 = await request3.Content.ReadAsStringAsync();
            var comparee = JsonConvert.DeserializeObject<List<ResultCompareDto>>(responseMessage3);



            var request = await client.GetAsync("https://localhost:7237/api/Product/");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
                products.ForEach(x => products.Select(x => x.IsFavourite == false));
                products.ForEach(x => products.Select(x => x.IsCompare == false));
                foreach (var item in products)
                {
                    foreach (var item2 in favori)
                    {
                        if (item.ID == item2.ProductID)
                        {
                            item.IsFavourite = true;
                        }
                    }
                    foreach (var item3 in comparee)
                    {
                        if (item.ID == item3.ProductID)
                        {
                            item.IsCompare = true;
                        }
                    }
                }
                ViewBag.count = products.Count();
                foreach(var item in products)
                {
                    var request4 = await client.GetAsync("https://localhost:7237/api/Comment/");
                    if (request4.IsSuccessStatusCode)
                    {
                        var responseMessage4 = await request4.Content.ReadAsStringAsync();
                        var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(responseMessage4);
                        var countrating = values.Where(x => x.ProductId == item.ID && x.Status == true).Select(x => x.Rating).Count();
                        var sumrating = values.Where(x => x.ProductId == item.ID && x.Status == true).Select(x => x.Rating).Sum();
                        if (countrating != 0)
                        {
                            item.Rate = (int)(sumrating / countrating);
                        }
                        else
                        {
                            item.Rate = 0;
                        }
                    }
                }



                
                return View(products);
            }
            return View();
        }


        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Index(string searchTerm)
        {
            if (searchTerm != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));





                var deger = UserHasScope();
                if (deger == false)
                {
                    var request5 = await client.GetAsync("https://localhost:7237/api/Product/");
                    if (request5.IsSuccessStatusCode)
                    {
                        var responseMessage = await request5.Content.ReadAsStringAsync();
                        products = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
                        products.ForEach(x => products.Select(x => x.IsFavourite == false));
                        products.ForEach(x => products.Select(x => x.IsCompare == false));
                        products = products.Where(x => x.ProductName.ToLower().Contains(searchTerm.ToLower())).ToList();
                        ViewBag.count = products.Count();
                        return View(products);
                    }
                }





                var request2 = await client.GetAsync("https://localhost:7237/api/Favourite/GetAllFavouriteWithProductByUserID");
                var responseMessage2 = await request2.Content.ReadAsStringAsync();
                var favori = JsonConvert.DeserializeObject<List<ResultFavouriteDto>>(responseMessage2);

                var request3 = await client.GetAsync("https://localhost:7237/api/Compare/GetAllCompareWithProductByUserID");
                var responseMessage3 = await request3.Content.ReadAsStringAsync();
                var comparee = JsonConvert.DeserializeObject<List<ResultCompareDto>>(responseMessage3);





                var request = await client.GetAsync("https://localhost:7237/api/Product/");
                if (request.IsSuccessStatusCode)
                {
                    var responseMessage = await request.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
                    products.ForEach(x => products.Select(x => x.IsFavourite == false));
                    products.ForEach(x => products.Select(x => x.IsCompare == false));
                    products = products.Where(x => x.ProductName.ToLower().Contains(searchTerm.ToLower())).ToList();
                    foreach (var item in products)
                    {
                        foreach (var item2 in favori)
                        {
                            if (item.ID == item2.ProductID)
                            {
                                item.IsFavourite = true;
                            }
                        }
                        foreach (var item3 in comparee)
                        {
                            if (item.ID == item3.ProductID)
                            {
                                item.IsCompare = true;
                            }
                        }
                    }
                    ViewBag.count = products.Count();
                    return View(products);
                }
            }
            return View();
        }


        [Route("arama")]
        [HttpGet]
        public async Task<IActionResult> Index2()
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token);
            }
            return View();
        }

        [Route("arama")]
        [HttpPost]
        public async Task<IActionResult> Index2(string searchTerm)
        {
           

            if (searchTerm != null)
            {
                var client2 = _httpClientFactory.CreateClient();
                client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

                var deger = UserHasScope();
                if(deger==false)
                {
                    var request5 = await client2.GetAsync("https://localhost:7237/api/Product/");
                    if (request5.IsSuccessStatusCode)
                    {
                        var responseMessage = await request5.Content.ReadAsStringAsync();
                        products = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
                        products.ForEach(x => products.Select(x => x.IsFavourite == false));
                        products.ForEach(x => products.Select(x => x.IsCompare == false));
                        products = products.Where(x => x.ProductName.ToLower().Contains(searchTerm.ToLower()) || x.Category.Name.ToLower().Contains(searchTerm.ToLower()) || x.Brand.ToLower().Contains(searchTerm.ToLower())).ToList();
                       
                        ViewBag.count = products.Count();
                        return View(products);
                    }
                }

                var request2 = await client2.GetAsync("https://localhost:7237/api/Favourite/GetAllFavouriteWithProductByUserID");
                var responseMessage2 = await request2.Content.ReadAsStringAsync();
                var favori = JsonConvert.DeserializeObject<List<ResultFavouriteDto>>(responseMessage2);


                var request3 = await client2.GetAsync("https://localhost:7237/api/Compare/GetAllCompareWithProductByUserID");
                var responseMessage3 = await request3.Content.ReadAsStringAsync();
                var comparee = JsonConvert.DeserializeObject<List<ResultCompareDto>>(responseMessage3);




                var request = await client2.GetAsync("https://localhost:7237/api/Product/");
                if (request.IsSuccessStatusCode)
                {
                    var responseMessage = await request.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
                    products.ForEach(x => products.Select(x => x.IsFavourite == false));
                    products.ForEach(x => products.Select(x => x.IsCompare == false));
                    products = products.Where(x => x.ProductName.ToLower().Contains(searchTerm.ToLower()) || x.Category.Name.ToLower().Contains(searchTerm.ToLower()) || x.Brand.ToLower().Contains(searchTerm.ToLower())).ToList();
                    foreach (var item in products)
                    {
                        foreach (var item2 in favori)
                        {
                            if (item.ID == item2.ProductID)
                            {
                                item.IsFavourite = true;
                            }
                        }
                        foreach (var item3 in comparee)
                        {
                            if (item.ID == item3.ProductID)
                            {
                                item.IsCompare = true;
                            }
                        }
                    }
                    ViewBag.count = products.Count();
                    return View(products);
                }
            }
            return View();
        }



        [Route("kategori/{categoryid}")]
        public async Task<IActionResult> Index(string categoryid, string null6, string null7, string null8, string null9, string null10)
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token);
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var deger = UserHasScope();
            if (deger == false)
            {
                var request5 = await client.GetAsync("https://localhost:7237/api/Product/GetProductsWithCategoryByCategoryId/" + categoryid);
                var responseMessage5 = await request5.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage5);
                products.ForEach(x => products.Select(x => x.IsFavourite == false));
                products.ForEach(x => products.Select(x => x.IsCompare == false));

                ViewBag.count = products.Count();
                return View(products);
            }


            var request2 = await client.GetAsync("https://localhost:7237/api/Favourite/GetAllFavouriteWithProductByUserID");
            var responseMessage2 = await request2.Content.ReadAsStringAsync();
            var favori = JsonConvert.DeserializeObject<List<ResultFavouriteDto>>(responseMessage2);


            var request3 = await client.GetAsync("https://localhost:7237/api/Compare/GetAllCompareWithProductByUserID");
            var responseMessage3 = await request3.Content.ReadAsStringAsync();
            var comparee = JsonConvert.DeserializeObject<List<ResultCompareDto>>(responseMessage3);



            var request = await client.GetAsync("https://localhost:7237/api/Product/GetProductsWithCategoryByCategoryId/" + categoryid);
            var responseMessage = await request.Content.ReadAsStringAsync();
            products = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
            products.ForEach(x => products.Select(x => x.IsFavourite == false));
            products.ForEach(x => products.Select(x => x.IsCompare == false));
            foreach (var item in products)
            {
                foreach (var item2 in favori)
                {
                    if (item.ID == item2.ProductID)
                    {
                        item.IsFavourite = true;
                    }
                }
                foreach (var item3 in comparee)
                {
                    if (item.ID == item3.ProductID)
                    {
                        item.IsCompare = true;
                    }
                }
            }



            ViewBag.count = products.Count();
            return View(products);


        }


        [Route("marka/{brand}")]
        public async Task<IActionResult> Index(string brand, string null2)
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token);
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var deger = UserHasScope();
            if (deger == false)
            {
                var request5 = await client.GetAsync("https://localhost:7237/api/Product/GetProductsWithCategoryByBrand/" + brand);
                var responseMessage5 = await request5.Content.ReadAsStringAsync();
                var products3 = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage5);
                products3.ForEach(x => products3.Select(x => x.IsFavourite == false));
                products3.ForEach(x => products3.Select(x => x.IsCompare == false));

                ViewBag.count = products3.Count();
                return View(products3);
            }


            var request2 = await client.GetAsync("https://localhost:7237/api/Favourite/GetAllFavouriteWithProductByUserID");
            var responseMessage2 = await request2.Content.ReadAsStringAsync();
            var favori = JsonConvert.DeserializeObject<List<ResultFavouriteDto>>(responseMessage2);


            var request3 = await client.GetAsync("https://localhost:7237/api/Compare/GetAllCompareWithProductByUserID");
            var responseMessage3 = await request3.Content.ReadAsStringAsync();
            var comparee = JsonConvert.DeserializeObject<List<ResultCompareDto>>(responseMessage3);



            var request = await client.GetAsync("https://localhost:7237/api/Product/GetProductsWithCategoryByBrand/" + brand);
            var responseMessage = await request.Content.ReadAsStringAsync();
            var products2 = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
            products2.ForEach(x => products2.Select(x => x.IsFavourite == false));
            products2.ForEach(x => products2.Select(x => x.IsCompare == false));


            List<ResultProductDto> values = (from p in products from x in products2 where p.ID == x.ID select p).ToList();
            products = values;
            foreach (var item in products)
            {
                foreach (var item2 in favori)
                {
                    if (item.ID == item2.ProductID)
                    {
                        item.IsFavourite = true;
                    }
                }
                foreach (var item3 in comparee)
                {
                    if (item.ID == item3.ProductID)
                    {
                        item.IsCompare = true;
                    }
                }
            }

            ViewBag.count = products.Count();
            return View(products);
        }



        [Route("price/{price}/{price2}")]
        public async Task<IActionResult> Index(double price, double price2, string null3)
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token);
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var deger = UserHasScope();
            if (deger == false)
            {
                var request5 = await client.GetAsync("https://localhost:7237/api/Product/GetProductsWithCategoryByPrice/" + price + "/" + price2);
                var responseMessage5 = await request5.Content.ReadAsStringAsync();
                var products25 = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage5);
                products25.ForEach(x => products25.Select(x => x.IsFavourite == false));
                products25.ForEach(x => products25.Select(x => x.IsCompare == false));

                ViewBag.count = products25.Count();
                return View(products25);
            }

            var request2 = await client.GetAsync("https://localhost:7237/api/Favourite/GetAllFavouriteWithProductByUserID");
            var responseMessage2 = await request2.Content.ReadAsStringAsync();
            var favori = JsonConvert.DeserializeObject<List<ResultFavouriteDto>>(responseMessage2);

            var request3 = await client.GetAsync("https://localhost:7237/api/Compare/GetAllCompareWithProductByUserID");
            var responseMessage3 = await request3.Content.ReadAsStringAsync();
            var comparee = JsonConvert.DeserializeObject<List<ResultCompareDto>>(responseMessage3);



            var request = await client.GetAsync("https://localhost:7237/api/Product/GetProductsWithCategoryByPrice/" + price + "/" + price2);
            var responseMessage = await request.Content.ReadAsStringAsync();
            var products2 = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
            products2.ForEach(x => products2.Select(x => x.IsFavourite == false));
            products2.ForEach(x => products2.Select(x => x.IsCompare == false));

            List<ResultProductDto> values = (from p in products from x in products2 where p.ID == x.ID select p).ToList();
            products = values;

            foreach (var item in products)
            {
                foreach (var item2 in favori)
                {
                    if (item.ID == item2.ProductID)
                    {
                        item.IsFavourite = true;
                    }
                }
                foreach (var item3 in comparee)
                {
                    if (item.ID == item3.ProductID)
                    {
                        item.IsCompare = true;
                    }
                }


            }

            ViewBag.count = products.Count();
            return View(products);
        }



        [Route("size/{size}")]
        public async Task<IActionResult> Index(string size, string null2, string null3, string null4)
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token);
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));



            var deger = UserHasScope();
            if (deger == false)
            {
                var request5 = await client.GetAsync("https://localhost:7237/api/Product/GetProductsWithCategoryBySize/" + size);
                var responseMessage5 = await request5.Content.ReadAsStringAsync();
                var products25 = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage5);
                products25.ForEach(x => products25.Select(x => x.IsFavourite == false));
                products25.ForEach(x => products25.Select(x => x.IsCompare == false));

                ViewBag.count = products25.Count();
                return View(products25);
            }




            var request2 = await client.GetAsync("https://localhost:7237/api/Favourite/GetAllFavouriteWithProductByUserID");
            var responseMessage2 = await request2.Content.ReadAsStringAsync();
            var favori = JsonConvert.DeserializeObject<List<ResultFavouriteDto>>(responseMessage2);

            var request3 = await client.GetAsync("https://localhost:7237/api/Compare/GetAllCompareWithProductByUserID");
            var responseMessage3 = await request3.Content.ReadAsStringAsync();
            var comparee = JsonConvert.DeserializeObject<List<ResultCompareDto>>(responseMessage3);




            var request = await client.GetAsync("https://localhost:7237/api/Product/GetProductsWithCategoryBySize/" + size);
            var responseMessage = await request.Content.ReadAsStringAsync();
            var products2 = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
            products2.ForEach(x => products2.Select(x => x.IsFavourite == false));
            products2.ForEach(x => products2.Select(x => x.IsCompare == false));

            List<ResultProductDto> values = (from p in products from x in products2 where p.ID == x.ID select p).ToList();
            products = values;

            foreach (var item in products)
            {
                foreach (var item2 in favori)
                {
                    if (item.ID == item2.ProductID)
                    {
                        item.IsFavourite = true;
                    }
                }
                foreach (var item3 in comparee)
                {
                    if (item.ID == item3.ProductID)
                    {
                        item.IsCompare = true;
                    }
                }
            }

            ViewBag.count = products.Count();
            return View(products);
        }

        [Route("renk/{ID}")]
        public async Task<IActionResult> Index(string ID, string null2, string null3, string null4, string null5)
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token);
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var deger = UserHasScope();
            if (deger == false)
            {

                var request5 = await client.GetAsync("https://localhost:7237/api/Product/GetProductsWithCategoryByColorId/" + ID);
                var responseMessage5 = await request5.Content.ReadAsStringAsync();
                var products25 = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage5);
                products25.ForEach(x => products25.Select(x => x.IsFavourite == false));
                products25.ForEach(x => products25.Select(x => x.IsCompare == false));

                ViewBag.count = products25.Count();
                return View(products25);
            }


            var request2 = await client.GetAsync("https://localhost:7237/api/Favourite/GetAllFavouriteWithProductByUserID");
            var responseMessage2 = await request2.Content.ReadAsStringAsync();
            var favori = JsonConvert.DeserializeObject<List<ResultFavouriteDto>>(responseMessage2);

            var request3 = await client.GetAsync("https://localhost:7237/api/Compare/GetAllCompareWithProductByUserID");
            var responseMessage3 = await request3.Content.ReadAsStringAsync();
            var comparee = JsonConvert.DeserializeObject<List<ResultCompareDto>>(responseMessage3);




            var request = await client.GetAsync("https://localhost:7237/api/Product/GetProductsWithCategoryByColorId/" + ID);
            var responseMessage = await request.Content.ReadAsStringAsync();
            var products2 = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
            products2.ForEach(x => products2.Select(x => x.IsFavourite == false));
            products2.ForEach(x => products2.Select(x => x.IsCompare == false));

            List<ResultProductDto> values = (from p in products from x in products2 where p.ID == x.ID select p).ToList();
            products = values;
            foreach (var item in products)
            {
                foreach (var item2 in favori)
                {
                    if (item.ID == item2.ProductID)
                    {
                        item.IsFavourite = true;
                    }
                }
                foreach (var item3 in comparee)
                {
                    if (item.ID == item3.ProductID)
                    {
                        item.IsCompare = true;
                    }
                }
            }


            ViewBag.count = products.Count();
            return View(products);
        }






        [Route("ürün-detay/{id}")]
        public async Task<IActionResult> ProductDetail(string id)
        {

            





            var token2 = HttpContext.Session.GetString("AuthToken");
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token2) as JwtSecurityToken;
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier);
            
           
          



            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token);
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Product/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetByIdProductDto>(responseMessage);
                ViewBag.productid = value.ID;

                if(userIdClaim!=default)
                {
                    var responseMessage2 = await client.GetAsync("https://localhost:5001/api/GetUserReal/" + userIdClaim.Value);
                    if (responseMessage2 != null)
                    {
                        var jsondata = await responseMessage2.Content.ReadAsStringAsync();
                        var uservalues = JsonConvert.DeserializeObject<_GetUserRealValue>(jsondata);
                        if (uservalues != null)
                        {
                            ViewBag.usernameforcomment = uservalues.Name + " " + uservalues.Surname;
                        }
                    }
                }
                else
                {
                    ViewBag.usernameforcomment = " Giriş Yapınız...";
                }




                var request2 = await client.GetAsync("https://localhost:7237/api/Comment/");
                if (request2.IsSuccessStatusCode)
                {
                    var responseMessage2 = await request2.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(responseMessage2);
                    var countrating = values.Where(x => x.ProductId == id && x.Status == true).Select(x => x.Rating).Count();
                    var sumrating = values.Where(x => x.ProductId == id && x.Status == true).Select(x => x.Rating).Sum();
                    if (countrating != 0)
                    {
                        ViewBag.ortalamarating = sumrating / countrating;
                        ViewBag.intrating = (int)(sumrating / countrating);
                        ViewBag.countrating = countrating;
                    }
                    else
                    {
                        ViewBag.ortalamarating =0;
                        ViewBag.intrating = 0;
                        ViewBag.countrating = countrating;
                    }
                }



                return View(value);
            }
            return View();
        }



        [Route("ürün-detay/renk/{colorid}/{productid}")]
        public async Task<IActionResult> ProductDetail(string colorid, string productid)
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token);
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Product/");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GetByIdProductDto>>(responseMessage);
                var value1 = values.Where(x => x.ID == productid).SingleOrDefault();
                var value2 = values.Where(x => x.ColorId == colorid && x.ProductName == value1.ProductName && x.TechnicalSpecifications["Size"] == value1.TechnicalSpecifications["Size"]).FirstOrDefault();

                if (value2 != null)
                {
                    return RedirectToAction("ürün-detay", "ürünler", new { id = value2.ID });
                }
                return RedirectToAction("ürün-detay", "ürünler", new { id = productid });

            }
            return View();
        }


        [Route("ürün-detay/boyut/{size}/{productid}")]
        public async Task<IActionResult> ProductDetail(string size, string productid, string null2)
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token);
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Product/");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GetByIdProductDto>>(responseMessage);
                var value1 = values.Where(x => x.ID == productid).SingleOrDefault();
                var value2 = values.Where(x => x.ColorId == value1.ColorId && x.ProductName == value1.ProductName && x.TechnicalSpecifications["Size"] == size).FirstOrDefault();
                if (value2 != null)
                {
                    return RedirectToAction("ürün-detay", "ürünler", new { id = value2.ID });
                }
                return RedirectToAction("ürün-detay", "ürünler", new { id = productid });
            }
            return View();
        }




        [Route("benzer-ürünler/{id}")]
        public async Task<IActionResult> SearchProduct(string id)
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token);
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));




            var deger = UserHasScope();
            if (deger == false)
            {

                var request25 = await client.GetAsync("https://localhost:7237/api/Product/" + id);

                var request5 = await client.GetAsync("https://localhost:7237/api/Product/");
                if (request5.IsSuccessStatusCode)
                {
                    var responseMessage2 = await request25.Content.ReadAsStringAsync();
                    var value = JsonConvert.DeserializeObject<GetByIdProductDto>(responseMessage2);
                    var responseMessage = await request5.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
                    products.ForEach(x => products.Select(x => x.IsFavourite == false));

                    products = products.Where(x => x.CategoryId == value.CategoryId && x.Brand == value.Brand).ToList();
                    
                    ViewBag.count = products.Count();
                    return View(products);
                }
            }



            var request3 = await client.GetAsync("https://localhost:7237/api/Favourite/GetAllFavouriteWithProductByUserID");
            var responseMessage3 = await request3.Content.ReadAsStringAsync();
            var favori = JsonConvert.DeserializeObject<List<ResultFavouriteDto>>(responseMessage3);



            var request4 = await client.GetAsync("https://localhost:7237/api/Compare/GetAllCompareWithProductByUserID");
            var responseMessage4 = await request4.Content.ReadAsStringAsync();
            var comparee = JsonConvert.DeserializeObject<List<ResultCompareDto>>(responseMessage4);






            var request2 = await client.GetAsync("https://localhost:7237/api/Product/" + id);

            var request = await client.GetAsync("https://localhost:7237/api/Product/");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage2 = await request2.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetByIdProductDto>(responseMessage2);
                var responseMessage = await request.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
                products.ForEach(x => products.Select(x => x.IsFavourite == false));

                products = products.Where(x => x.CategoryId == value.CategoryId && x.Brand == value.Brand).ToList();
                foreach (var item in products)
                {
                    foreach (var item2 in favori)
                    {
                        if (item.ID == item2.ProductID)
                        {
                            item.IsFavourite = true;
                        }
                    }
                    foreach (var item3 in comparee)
                    {
                        if (item.ID == item3.ProductID)
                        {
                            item.IsCompare = true;
                        }
                    }
                }
                ViewBag.count = products.Count();
                return View(products);
            }
            return View();
        }
    }
}


