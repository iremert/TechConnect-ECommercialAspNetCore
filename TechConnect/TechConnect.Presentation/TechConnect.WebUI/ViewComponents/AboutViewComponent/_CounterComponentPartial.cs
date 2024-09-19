using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechConnect.DtoUI.CategoryDtos;
using TechConnect.DtoUI.OrderingDtos;
using TechConnect.DtoUI.ProductDto;

namespace TechConnect.WebUI.ViewComponents.AboutViewComponent
{
    public class _CounterComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CounterComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.kategori = 0;
            ViewBag.ürün = 0;
            ViewBag.sipariş = 0;
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));



            var request = await client.GetAsync("https://localhost:7237/api/Category/");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(responseMessage);
                ViewBag.kategori = values.Count();
            }

            var request2 = await client.GetAsync("https://localhost:7237/api/Product/");
            if (request2.IsSuccessStatusCode)
            {
                var responseMessage = await request2.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
                values=values.Where(x => x.IsAvailable == true).ToList();
                ViewBag.ürün = values.Count();
            }

            var request3 = await client.GetAsync("https://localhost:7237/api/Ordering/");
            if (request3.IsSuccessStatusCode)
            {
                var responseMessage = await request3.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultOrderingDto>>(responseMessage);
                
                ViewBag.sipariş = values.Count();
            }


            return View();
        }
    }
}
