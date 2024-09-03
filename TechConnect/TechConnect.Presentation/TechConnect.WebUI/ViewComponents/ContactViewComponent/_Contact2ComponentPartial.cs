using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechConnect.DtoUI.Contact2Dtos;

namespace TechConnect.WebUI.ViewComponents.ContactViewComponent
{
    public class _Contact2ComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _Contact2ComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Contact2/");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContact2Dto>>(responseMessage);
                return View(values);
            }
            return View();
        }
    }
}
