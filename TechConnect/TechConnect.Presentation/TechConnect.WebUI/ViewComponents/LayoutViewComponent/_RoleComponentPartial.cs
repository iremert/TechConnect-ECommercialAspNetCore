using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TechConnect.WebUI.Models;

namespace TechConnect.WebUI.ViewComponents.LayoutViewComponent
{
    public class _RoleComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _RoleComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            // Payload kısmına erişim
            var claims = jwtToken.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                ViewBag.a = "a";
            }
            else if(scopes.Contains("AdminPermission"))
            {
                ViewBag.a = "c";
                var handler = new JwtSecurityTokenHandler();
                var jwtToken2 = handler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken2 == null)
                {
                    return View();
                }
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier);

                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

                var responseMessage = await client.GetAsync("https://localhost:5001/api/GetUserReal/" + userIdClaim.Value);
                if (responseMessage != null)
                {
                    var jsondata = await responseMessage.Content.ReadAsStringAsync();
                    var uservalues = JsonConvert.DeserializeObject<_GetUserRealValue>(jsondata);
                    if (uservalues != null)
                    {
                        ViewBag.kullanıcı = uservalues.Name + " " + uservalues.Surname;
                        ViewBag.kullanıcıid = uservalues.Id;
                    }
                }
            }
            else
            {
                ViewBag.a = "b";
                
                var handler = new JwtSecurityTokenHandler();
                var jwtToken2 = handler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken2 == null)
                {
                    return View();
                }
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier);

                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

                var responseMessage = await client.GetAsync("https://localhost:5001/api/GetUserReal/" + userIdClaim.Value);
                if (responseMessage != null)
                {
                    var jsondata = await responseMessage.Content.ReadAsStringAsync();
                    var uservalues = JsonConvert.DeserializeObject<_GetUserRealValue>(jsondata);
                    if (uservalues != null)
                    {
                        ViewBag.kullanıcı =uservalues.Name + " " + uservalues.Surname;
                        ViewBag.kullanıcıid = uservalues.Id;
                    }
                }
            }

            return View();
        }
    }
}
