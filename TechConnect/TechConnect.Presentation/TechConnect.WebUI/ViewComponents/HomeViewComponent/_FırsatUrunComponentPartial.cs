using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechConnect.DtoUI.ProductDto;
using TechConnect.EL.Concrete;

namespace TechConnect.WebUI.ViewComponents.HomeViewComponent
{
    public class _FırsatUrunComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FırsatUrunComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request5 = await client.GetAsync("https://localhost:7237/api/Product/");
            if (request5.IsSuccessStatusCode)
            {
                var responseMessage = await request5.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<GetByIdProductDto>>(responseMessage);
                var product = products.Where(x => x.IsAvailable == true).FirstOrDefault();
                
                return View(product);

            }
            return View();
        }
    }
}
