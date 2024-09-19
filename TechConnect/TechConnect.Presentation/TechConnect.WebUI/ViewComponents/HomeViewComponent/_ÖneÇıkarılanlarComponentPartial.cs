using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechConnect.DtoUI.ProductDto;

namespace TechConnect.WebUI.ViewComponents.HomeViewComponent
{
    public class _ÖneÇıkarılanlarComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ÖneÇıkarılanlarComponentPartial(IHttpClientFactory httpClientFactory)
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
                var products = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
                var product = products.Where(x => x.IsFeatured == true).Take(3).ToList();
                return View(product);

            }
            return View();
        }
    }
}
