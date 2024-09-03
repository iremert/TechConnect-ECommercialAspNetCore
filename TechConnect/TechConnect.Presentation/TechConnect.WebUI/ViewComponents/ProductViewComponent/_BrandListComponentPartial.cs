using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechConnect.DtoUI.BrandDtos;
using TechConnect.DtoUI.ProductDto;

namespace TechConnect.WebUI.ViewComponents.ProductViewComponent
{
    public class _BrandListComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _BrandListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Product/");
            if(request.IsSuccessStatusCode)
            {
                var responseMessage= await request.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage);
                var brand = new ResultBrandDto();

                var brand2 = values.Select(x => x.Brand).Distinct();
                foreach (var item in brand2)
                {
                    brand.BrandNames.Add(item);
                }
                
                return View(brand);
            }

            return View();
        }
    }
}
