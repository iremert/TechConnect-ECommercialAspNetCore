using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TechConnect.WebUI.Models;

namespace TechConnect.WebUI.ViewComponents.AdminLayoutViewComponent
{
    public class _AdminNameComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _AdminNameComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var token = HttpContext.Session.GetString("AuthToken");
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null)
            {
                return View();
            }
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim.Value))
            {
                return View();
            }



            var responseMessage = await client.GetAsync("https://localhost:5001/api/GetUserReal/"+userIdClaim.Value);
            if (responseMessage != null)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var uservalues = JsonConvert.DeserializeObject<_GetUserRealValue>(jsondata);
                ViewBag.username = uservalues.Name + " " + uservalues.Surname;
                return View();
            }
            return View();
        }
    }
}
