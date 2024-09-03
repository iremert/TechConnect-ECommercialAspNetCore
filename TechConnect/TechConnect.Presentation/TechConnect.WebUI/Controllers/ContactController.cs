using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TechConnect.DtoUI.ContactDtos;
using TechConnect.DtoUI.IdentityDtos.LoginDtos;
using TechConnect.EL.Concrete;
using TechConnect.WebUI.Services.Concrete;

namespace TechConnect.WebUI.Controllers
{
    [Route("iletişim")]
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }



        [Route("mesaj-ekle")]
        [HttpGet]
        public PartialViewResult AddContact()
        {
            return PartialView();
        }


        [Route("mesaj-ekle")]
        [HttpPost]
        public async Task<IActionResult> AddContact(CreateContactDto createContactDto )
        {
            createContactDto.Status = false;
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var jsonData = JsonConvert.SerializeObject(createContactDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
            var responseMessage = await client.PostAsync("https://localhost:7237/api/Contact/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("","iletişim");
            }
            return View();
        }
    }
}
