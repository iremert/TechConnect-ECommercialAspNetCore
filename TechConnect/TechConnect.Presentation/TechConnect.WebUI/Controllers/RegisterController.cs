using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Text;
using TechConnect.DtoUI.IdentityDtos.LoginDtos;
using TechConnect.DtoUI.IdentityDtos.RegisterDtos;
using TechConnect.WebUI.Services.Interfaces;

namespace TechConnect.WebUI.Controllers
{
    [Route("Kayıt-Ol")]
    public class RegisterController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Index(CreateRegisterDto createRegisterDto)
        {
        //    createRegisterDto.Name = "aa";
        //    createRegisterDto.Surname = "aa";
        //    createRegisterDto.Username = "aaa";
        //    createRegisterDto.Password = "Akademi.20";
        //    createRegisterDto.Email = "iremerturk@gmail.com";
        //    createRegisterDto.ConfirmPassword= "Akademi.20";

            if (createRegisterDto.Password == createRegisterDto.ConfirmPassword)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


                var jsonData = JsonConvert.SerializeObject(createRegisterDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:5001/api/Registers", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("", "Giriş-Yap");
                }
                else
                {
                    var hata=responseMessage.Content.ReadAsStringAsync().Result;
                    ViewBag.Message = hata;
                }
            }
            else
            {
                ViewBag.hata = "Şifreler uyuşmuyor , tekrar deneyin";
            }
            return View();



           
            
        }
    }
}
