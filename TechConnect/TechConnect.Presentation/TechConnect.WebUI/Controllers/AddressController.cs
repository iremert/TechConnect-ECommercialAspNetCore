using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechConnect.DtoUI.AddressDtos;
using TechConnect.EL.Concrete;
using TechConnect.WebUI.Services.Concrete;

namespace TechConnect.WebUI.Controllers
{
//    [Authorize(Roles = "User")]
    [Route("")]
    public class AddressController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AddressController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Adres-Ekle")]
        [HttpGet]
        public async Task<IActionResult> AddAddress()
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token2 = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token2);
            }
            var token = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            // Payload kısmına erişim
            var claims = jwtToken.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }
            return View();
        }


        [Route("Adres-Ekle")]
        [HttpPost]
        public async  Task<IActionResult> AddAddress(CreateAddressDto createAddressDto)
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token2 = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token2);
            }
            var token3 = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken3 = tokenHandler.ReadJwtToken(token3);
            // Payload kısmına erişim
            var claims = jwtToken3.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }



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


            createAddressDto.UserId = userIdClaim.Value;
            var jsonData = JsonConvert.SerializeObject(createAddressDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); 
            var responseMessage = await client.PostAsync("https://localhost:7237/api/Address/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("adres-işlemleri", "sipariş-sepeti");
            }
            return View();
        }








        [Route("Adres-Düzenle/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateAddress(string id)
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token2 = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token2);
            }
            var token3 = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken3 = tokenHandler.ReadJwtToken(token3);
            // Payload kısmına erişim
            var claims = jwtToken3.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }


            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var request11 = await client.GetAsync("https://localhost:7237/api/Address/" + id);
            var responseMessage11 = await request11.Content.ReadAsStringAsync();
            var values11 = JsonConvert.DeserializeObject<GetByIdAddressDto>(responseMessage11);

            return View(values11);
        }


        [Route("Adres-Düzenle/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateAddress(UpdateAddressDto updateAddressDto)
        {
            if (HttpContext.Session.GetString("AuthToken") == null)
            {
                string token2 = await CreateVisitorToken.GetTokenAsync(_httpClientFactory);
                HttpContext.Session.SetString("AuthToken", token2);
            }
            var token3 = HttpContext.Session.GetString("AuthToken");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken3 = tokenHandler.ReadJwtToken(token3);
            // Payload kısmına erişim
            var claims = jwtToken3.Claims;

            // `scope` claim'ini almak
            var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
            if (!scopes.Contains("UserPermission"))
            {
                return Unauthorized();
            }


            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));



            var jsondata = JsonConvert.SerializeObject(updateAddressDto);
            StringContent stringcontent = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7237/api/Address/"+updateAddressDto.ID, stringcontent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("adres-işlemleri", "sipariş-sepeti");
            }
            return View();
        }
    }
}
