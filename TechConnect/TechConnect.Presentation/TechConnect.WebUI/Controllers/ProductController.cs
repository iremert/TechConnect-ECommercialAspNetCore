using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechConnect.DtoUI.CompareDtos;
using TechConnect.DtoUI.FavouriteDtos;
using TechConnect.DtoUI.ProductDto;
using TechConnect.EL.Concrete;

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

        [Route("")]
        public async Task<IActionResult> Index()
        {

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


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
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

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
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


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
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));



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
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));




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
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));




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
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Product/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetByIdProductDto>(responseMessage);
                return View(value);
            }
            return View();
        }



        [Route("ürün-detay/renk/{colorid}/{productid}")]
        public async Task<IActionResult> ProductDetail(string colorid, string productid)
        {
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
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


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


