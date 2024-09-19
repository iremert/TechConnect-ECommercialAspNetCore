using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TechConnect.DtoUI.CommentDtos;

namespace TechConnect.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CommentController(IHttpClientFactory httpClientFactory)
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


            var request = await client.GetAsync("https://localhost:7237/api/Comment");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var Comments = JsonConvert.DeserializeObject<List<ResultCommentDto>>(responseMessage);
                return View(Comments);
            }
            return View();
        }





        

        public async Task<IActionResult> DeleteComment(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));
            var responseMessage = await client.DeleteAsync($"https://localhost:7237/api/Comment/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var url2 = "/Admin/Comment/Index/";
                return Redirect(url2);
            }
            var url = "/Admin/Comment/Index/";
            return Redirect(url);
        }



        public async Task<IActionResult> DoActiveComment(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Comment/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage2 = await request.Content.ReadAsStringAsync();
                var Comment = JsonConvert.DeserializeObject<GetByIdCommentDto>(responseMessage2);
                Comment.Status = true;
                var jsonData = JsonConvert.SerializeObject(Comment);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var responseMessage = await client.PutAsync("https://localhost:7237/api/Comment/" + id, stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var url2 = "/Admin/Comment/Index/";
                    return Redirect(url2);
                }
            }




            var url = "/Admin/Comment/Index/";
            return Redirect(url);
        }

        public async Task<IActionResult> DoPassiveComment(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Comment/" + id);
            if (request.IsSuccessStatusCode)
            {
                var responseMessage2 = await request.Content.ReadAsStringAsync();
                var Comment = JsonConvert.DeserializeObject<GetByIdCommentDto>(responseMessage2);
                Comment.Status = false;
                var jsonData = JsonConvert.SerializeObject(Comment);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
                var responseMessage = await client.PutAsync("https://localhost:7237/api/Comment/" + id, stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var url2 = "/Admin/Comment/Index/";
                    return Redirect(url2);
                }
            }




            var url = "/Admin/Comment/Index/";
            return Redirect(url);
        }
    }
}
