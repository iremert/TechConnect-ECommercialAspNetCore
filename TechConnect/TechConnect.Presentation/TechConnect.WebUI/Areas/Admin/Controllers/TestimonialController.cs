using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TechConnect.DtoUI.TestimonialDtos;

namespace TechConnect.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimonialController(IHttpClientFactory httpClientFactory)
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


            var request = await client.GetAsync("https://localhost:7237/api/Testimonial");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var testimonial = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(responseMessage);
                return View(testimonial);
            }
            return View();
        }





        [HttpGet]
        public IActionResult AddTestimonial()
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            createTestimonialDto.Status = false;


            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(createTestimonialDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
            var responseMessage = await client.PostAsync("https://localhost:7237/api/Testimonial/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var url = "/Admin/Testimonial/Index/";
                return Redirect(url);
            }
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(string id)
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


            var request = await client.GetAsync("https://localhost:7237/api/Testimonial/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var testimonial = JsonConvert.DeserializeObject<GetByIdTestimonialDto>(responseMessage);
                return View(testimonial);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(GetByIdTestimonialDto testimonialDto)
        {

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(testimonialDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
            var responseMessage = await client.PutAsync("https://localhost:7237/api/Testimonial/" + testimonialDto.ID, stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var url = "/Admin/Testimonial/Index/";
                return Redirect(url);
            }
            return View();

        }



        public async Task<IActionResult> DeleteTestimonial(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));
            var responseMessage = await client.DeleteAsync($"https://localhost:7237/api/Testimonial/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var url2 = "/Admin/Testimonial/Index/";
                return Redirect(url2);
            }
            var url = "/Admin/Testimonial/Index/";
            return Redirect(url);
        }



        public async Task<IActionResult> DoActiveTestimonial(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Testimonial/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage2 = await request.Content.ReadAsStringAsync();
                var Testimonial = JsonConvert.DeserializeObject<GetByIdTestimonialDto>(responseMessage2);
                Testimonial.Status = true;
                var jsonData = JsonConvert.SerializeObject(Testimonial);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var responseMessage = await client.PutAsync("https://localhost:7237/api/Testimonial/" + id, stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var url2 = "/Admin/Testimonial/Index/";
                    return Redirect(url2);
                }
            }




            var url = "/Admin/Testimonial/Index/";
            return Redirect(url);
        }

        public async Task<IActionResult> DoPassiveTestimonial(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Testimonial/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage2 = await request.Content.ReadAsStringAsync();
                var Testimonial = JsonConvert.DeserializeObject<GetByIdTestimonialDto>(responseMessage2);
                Testimonial.Status = false;
                var jsonData = JsonConvert.SerializeObject(Testimonial);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var responseMessage = await client.PutAsync("https://localhost:7237/api/Testimonial/" + id, stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var url2 = "/Admin/Testimonial/Index/";
                    return Redirect(url2);
                }
            }




            var url = "/Admin/Testimonial/Index/";
            return Redirect(url);
        }
    }
}
