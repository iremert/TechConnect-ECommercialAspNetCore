using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechConnect.DtoUI.CommentDtos;
using TechConnect.EL.Concrete;
using TechConnect.WebUI.Models;

namespace TechConnect.WebUI.Controllers
{
    
    public class CommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<PartialViewResult> AddComment(string id)
        {
            

            ViewBag.productid=id;
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto)
        {
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


            createCommentDto.Status = true;
            createCommentDto.CreatedDate = DateTime.Now;
            createCommentDto.UserId = userIdClaim.Value;
            createCommentDto.ProductId=createCommentDto.ProductId;
           

            



            var jsonData = JsonConvert.SerializeObject(createCommentDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //sondaki medya türüdür,doğru çevrilmesi için gereklidir.
            var responseMessage2 = await client.PostAsync("https://localhost:7237/api/Comment/", stringContent);
            if (responseMessage2.IsSuccessStatusCode)
            {
                return RedirectToAction("ürün-detay","ürünler",new {id=createCommentDto.ProductId});
            }
            return RedirectToAction("", "ürünler");
        }

    }
}
