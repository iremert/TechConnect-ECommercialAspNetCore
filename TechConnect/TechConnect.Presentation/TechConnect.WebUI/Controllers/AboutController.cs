using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using TechConnect.DtoUI.AboutDtos;
using TechConnect.DtoUI.IdentityDtos.LoginDtos;
using TechConnect.WebUI.Services.Concrete;
using TechConnect.WebUI.Services.Interfaces;


namespace TechConnect.WebUI.Controllers
{
   
    [Route("hakkımızda")]
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;

        public AboutController(IHttpClientFactory httpClientFactory,ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            if(HttpContext.Session.GetString("AuthToken")==null)
            {
                string token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token);
            }
           


            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));
       


            var request = await client.GetAsync("https://localhost:7237/api/About/");
            if(request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultAboutDto>>(responseMessage);
                values = values.Where(x => x.Status == true).ToList();
                return View(values);
            }
            return View();
        }
    }
}
