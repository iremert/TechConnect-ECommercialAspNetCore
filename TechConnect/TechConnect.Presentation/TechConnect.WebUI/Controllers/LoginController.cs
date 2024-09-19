using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using TechConnect.DtoUI.IdentityDtos.LoginDtos;
using TechConnect.WebUI.Services.Interfaces;
using TechConnect.WebUI.Models;
using TechConnect.WebUI.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using TechConnect.IdentityServer.Models;

namespace TechConnect.WebUI.Controllers
{
    [Route("Giriş-Yap")]
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;


        public LoginController(IHttpClientFactory httpClientFactory, IIdentityService identityService, ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _identityService = identityService;
            _tokenService = tokenService;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Index(SignInDto signInDto)
        {
            
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(signInDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:5001/api/Logins", stringContent);
            if(responseMessage.IsSuccessStatusCode)
            {
                var jsondata  = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<SignInDto>(jsondata);
                if(values==null)
                { return View(); }
                signInDto.Username = values.Username;

                var jsonData2 = JsonConvert.SerializeObject(signInDto);
                StringContent stringContent2 = new StringContent(jsonData2, Encoding.UTF8, "application/json");
                var responseMessage2 = await client.PostAsync("https://localhost:5001/api/Logins2", stringContent2);
                if (responseMessage2.IsSuccessStatusCode)
                {

                    ViewBag.message = "";
                    string token = await HttpClientExtensions.GetTokenAsync(_httpClientFactory, signInDto);


                    HttpContext.Session.SetString("AuthToken", token);

                    return RedirectToAction("Index", "Homee");
                }
                else
                {
                    ViewBag.message = "Girdiğiniz şifre veya mail yanlış , lütfen tekrar deneyiniz.";
                    return View();
                }
               
            }
            else
            {
                ViewBag.message = "Olmayan bir email girdiniz,lütfen girdiğiniz bilgileri kontrol ediniz.";
                return View();
            }
          


           

        }




        [Route("Şifremi-unuttum")]
        [HttpGet]
        public IActionResult Index2()
        {
            return View();
        }


        [Route("Şifremi-unuttum")]
        [HttpPost]
        public async Task<IActionResult> Index2(SignInDto signInDto)
        {
            return View();

        }




        [Route("")]
        public IActionResult AccessDenied()
        {
            return View();
        }



        [Route("çıkış-yap")]
        public async Task<IActionResult> LogOut()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            await client.GetAsync("https://localhost:5001/api/LogOut/");
            string token = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
            HttpContext.Session.SetString("AuthToken", token);
            return RedirectToAction("Index","Homee");
        }
    }
}
