using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechConnect.DtoUI.CategoryDtos;
using TechConnect.DtoUI.ProductDto;

namespace TechConnect.WebUI.ViewComponents.ProductViewComponent
{
    public class _CategoryListComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CategoryListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request =await client.GetAsync("https://localhost:7237/api/Category/");
           
            if(request.IsSuccessStatusCode)
            {
                var responseMessage=await request.Content.ReadAsStringAsync();
                var values= JsonConvert.DeserializeObject<List<ResultCategoryDto>>(responseMessage);
                foreach(var item in values)
                {
                    var request2 = await client.GetAsync("https://localhost:7237/api/Product/GetProductsWithCategoryByCategoryId/"+item.ID);
                    if(request2.IsSuccessStatusCode)
                    {
                        var responseMessage2 = await request2.Content.ReadAsStringAsync();
                        var values2 = JsonConvert.DeserializeObject<List<ResultProductDto>>(responseMessage2);
                        item.Count= values2.Count();
                    }
                }
                
                return View(values);
            }
            return View();
        }
    }
}
