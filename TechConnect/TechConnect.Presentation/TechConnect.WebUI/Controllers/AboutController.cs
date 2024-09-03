using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using TechConnect.DtoUI.AboutDtos;
using TechConnect.DtoUI.IdentityDtos.LoginDtos;
using TechConnect.WebUI.Services.Concrete;
using TechConnect.WebUI.Services.Interfaces;


namespace TechConnect.WebUI.Controllers
{
    [Route("hakkımızda")]
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;

        public AboutController(IHttpClientFactory httpClientFactory,ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {


            //var client2 = _httpClientFactory.CreateClient();
            ////var token = await HttpContext.GetTokenAsync("access_token");




            //string token = "";
            //using (var httpClient = new HttpClient())
            //{
            //    var request2 = new HttpRequestMessage
            //    {
            //        RequestUri = new Uri("https://localhost:5001/connect/token"), //bir apiden jwt token alır ve bu tokenı kullanarak başka apiye yetkilendirilmiş istek yapar
            //        Method = HttpMethod.Post,
            //        Content = new FormUrlEncodedContent(new Dictionary<string, string>
            //        {
            //            {"client_id","TechConnectVisitorId" },
            //            {"client_secret","techconnectsecret" },
            //            {"grant_type","client_credentials" }
            //        })
            //    };

            //    using (var response = await httpClient.SendAsync(request2)) //token alma başarılıysa yanıtın içeriği ayrıştırılır ve token alınır..
            //    {
            //        if (response.IsSuccessStatusCode)
            //        {
            //            var content = await response.Content.ReadAsStringAsync();
            //            var tokenResponse = JObject.Parse(content);
            //            token = tokenResponse["access_token"].ToString();
            //        }
            //    }
            //}





            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));
       


            var request = await client.GetAsync("https://localhost:7237/api/About/");
            if(request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultAboutDto>>(responseMessage);
               
                return View(values);
            }
            return View();
        }
    }
}
