using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechConnect.DtoUI.CategoryDtos;
using TechConnect.DtoUI.ProductDto;

namespace TechConnect.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ChooseSizeComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public _ChooseSizeComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string categoryid, string id)
        {
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

                var request = await client.GetAsync("https://localhost:7237/api/Category/");
                var request2 = await client.GetAsync("https://localhost:7237/api/Product/" + id);
                if (request.IsSuccessStatusCode)
                {
                    var responseMessage = await request.Content.ReadAsStringAsync();
                    var responseMessage2 = await request2.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(responseMessage);
                    var values2 = JsonConvert.DeserializeObject<GetByIdProductDto>(responseMessage2);
                    var value = values.Where(x => x.ID == categoryid).SingleOrDefault();
                    ViewBag.id = id;
                    ViewBag.sizenow = values2.TechnicalSpecifications["Size"];
                    return View(value);
                }

                return View();
            }
        }
    }
}
