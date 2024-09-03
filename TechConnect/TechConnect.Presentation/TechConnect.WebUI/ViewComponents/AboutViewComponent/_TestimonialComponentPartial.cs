using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechConnect.DtoUI.IdentityDtos.LoginDtos;
using TechConnect.DtoUI.TestimonialDtos;
using TechConnect.WebUI.Services.Concrete;
using TechConnect.WebUI.Services.Interfaces;

namespace TechConnect.WebUI.ViewComponents.AboutViewComponent
{
    public class _TestimonialComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;

        public _TestimonialComponentPartial(IHttpClientFactory httpClientFactory,ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var request = await client.GetAsync("https://localhost:7237/api/Testimonial/");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(responseMessage);
                return View(values);
            }
            return View();
        }
    }
}
