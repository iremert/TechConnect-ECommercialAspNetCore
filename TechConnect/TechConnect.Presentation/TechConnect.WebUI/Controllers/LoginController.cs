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

namespace TechConnect.WebUI.Controllers
{
    [Route("Giriş-Yap")]
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public LoginController(IHttpClientFactory httpClientFactory, IIdentityService identityService,ITokenService tokenService)
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
            if(responseMessage!=null)
            {
                var jsondata  = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<SignInDto>(jsondata);
                if(values==null)
                { return View(); }
                signInDto.Username = values.Username;
            }
            else
            {
                return View();
            }
           


            string token =await HttpClientExtensions.GetTokenAsync(_httpClientFactory, signInDto);
           

            HttpContext.Session.SetString("AuthToken", token);
            
            return RedirectToAction("", "hakkımızda");

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

    }
}
