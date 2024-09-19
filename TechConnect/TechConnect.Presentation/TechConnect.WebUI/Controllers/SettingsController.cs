using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechConnect.DtoUI.IdentityDtos.LoginDtos;
using TechConnect.IdentityServer.Models;
using TechConnect.WebUI.Models;
using TechConnect.WebUI.Services.Concrete;

namespace TechConnect.WebUI.Controllers
{
    [Route("ayarlar")]
    public class SettingsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SettingsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var responseMessage = await client.GetAsync("https://localhost:5001/api/GetUserReal/" + id);
            if (responseMessage != null)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var uservalues = JsonConvert.DeserializeObject<_GetUserRealValue>(jsondata);
                if (uservalues != null)
                {
                    _SettingsUser settingsUser = new _SettingsUser();
                    settingsUser.UserId = id;
                    settingsUser.Email = uservalues.Email;
                    settingsUser.Surname = uservalues.Surname;
                    settingsUser.Username = uservalues.Surname;
                    settingsUser.Name = uservalues.Name;
                    return View(settingsUser);
                }
            }

            return View();
        }



        [Route("{id}")]
        [HttpPost]
        public async Task<IActionResult> Index(_SettingsUser settingsUser)
        {
            var token = "";
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token);
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var responseMessage = await client.GetAsync("https://localhost:5001/api/GetUserReal/" + settingsUser.UserId);
            if (responseMessage != null)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var uservalues = JsonConvert.DeserializeObject<ApplicationUser>(jsondata);
                if (uservalues != null)
                {
                    if (settingsUser.Password != null & settingsUser.ConfirmPassword != null)
                    {

                        if (settingsUser.ConfirmPassword == settingsUser.Password)
                        {
                            var client2 = _httpClientFactory.CreateClient();
                            var jsonData = JsonConvert.SerializeObject(settingsUser);
                            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                            var responseMessage2 = await client2.PostAsync("https://localhost:5001/api/SettingsChangePassword", stringContent);
                            if (responseMessage2.IsSuccessStatusCode)
                            {
                                return RedirectToAction("", "giriş-yap");
                            }
                        }
                        else
                        {
                            ViewBag.cevap = "Şireleriniz uyumlu değil.";
                        }
                    }
                }
            }

            return View();
        }
    }
}
