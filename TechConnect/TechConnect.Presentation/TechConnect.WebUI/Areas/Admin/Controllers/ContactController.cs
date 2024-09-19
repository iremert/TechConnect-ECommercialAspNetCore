using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TechConnect.DtoUI.ContactDtos;

namespace TechConnect.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            // Payload kısmına erişim
            var claims = jwtToken.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("AdminPermission"))
            {
                return Unauthorized();
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Contact");
            if (request.IsSuccessStatusCode)
            {
                var responseContact = await request.Content.ReadAsStringAsync();
                var Contacts = JsonConvert.DeserializeObject<List<ResultContactDto>>(responseContact);
                return View(Contacts);
            }
            return View();
        }


        public async Task<IActionResult> DeleteContact(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));
            var responseContact = await client.DeleteAsync($"https://localhost:7237/api/Contact/{id}");
            if (responseContact.IsSuccessStatusCode)
            {
                var url2 = "/Admin/Contact/Index/";
                return Redirect(url2);
            }
            var url = "/Admin/Contact/Index/";
            return Redirect(url);
        }



        public async Task<IActionResult> OpenMessage(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Contact/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseContact2 = await request.Content.ReadAsStringAsync();
                var Contact = JsonConvert.DeserializeObject<GetByIdContactDto>(responseContact2);
                Contact.Status = true;
                var jsonData = JsonConvert.SerializeObject(Contact);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var responseContact = await client.PutAsync("https://localhost:7237/api/Contact/" + id, stringContent);
                return View(Contact);
            }

            return View();
        }

       
    }
}
