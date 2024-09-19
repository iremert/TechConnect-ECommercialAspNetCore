using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using TechConnect.DtoUI.SliderDtos;

namespace TechConnect.WebUI.ViewComponents.HomeViewComponent
{
    public class _SliderComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _SliderComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request5 = await client.GetAsync("https://localhost:7237/api/Slider/");
            if (request5.IsSuccessStatusCode)
            {
                var responseMessage = await request5.Content.ReadAsStringAsync();
                var sliders = JsonConvert.DeserializeObject<List<ResultSliderDto>>(responseMessage);
                return View(sliders);

            }
            return View();
        }
    }
}
