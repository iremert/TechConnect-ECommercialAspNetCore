using Microsoft.AspNetCore.Mvc;
using TechConnect.WebUI.Services.Concrete;

namespace TechConnect.WebUI.Controllers
{
    public class HomeeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token2 = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token2);
            }
            return View();
        }
    }
}
