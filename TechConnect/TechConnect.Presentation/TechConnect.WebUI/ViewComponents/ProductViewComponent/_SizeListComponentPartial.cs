using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechConnect.DtoUI.CategoryDtos;

namespace TechConnect.WebUI.ViewComponents.ProductViewComponent
{
    public class _SizeListComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _SizeListComponentPartial(IHttpClientFactory httpClientFactory)
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
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(responseMessage);
                return View(values);
            }

            return View();
        }
    }
}
