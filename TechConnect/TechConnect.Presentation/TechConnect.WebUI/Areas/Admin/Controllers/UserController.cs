using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TechConnect.DtoUI.AddressDtos;
using TechConnect.WebUI.Areas.Admin.Models;
using TechConnect.WebUI.Models;

namespace TechConnect.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
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





            var responseMessage = await client.GetAsync("https://localhost:5001/api/GetUsersReal/");
            if (responseMessage != null)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var uservalues = JsonConvert.DeserializeObject<List<_GetUserRealValue2>>(jsondata);
                return View(uservalues);
            }

            return View();
        }







        public async Task<IActionResult> DeleteUser(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));
            var responseMessage = await client.GetAsync("https://localhost:5001/api/DeleteUser/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var url2 = "/Admin/User/Index/";
                return Redirect(url2);
            }
            var url = "/Admin/User/Index/";
            return Redirect(url);
        }



        [HttpGet]
        public async Task<IActionResult> AddressDetail(string id)
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


            var request = await client.GetAsync("https://localhost:7237/api/Address/");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var Slider = JsonConvert.DeserializeObject<List<GetByIdAddressDto>>(responseMessage);
                var address=Slider.Where(x => x.UserId == id).SingleOrDefault();
                return View(address);
            }
            return View();
        }






        //[HttpGet]
        //public async Task<IActionResult> AssignRole(int id)
        //{
        //    var token = HttpContext.Session.GetString("AuthToken");
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var jwtToken = tokenHandler.ReadJwtToken(token);
        //    // Payload kısmına erişim
        //    var claims = jwtToken.Claims;

        //    // `scope` claim'ini almak
        //    var scopes = claims.Where(c => c.Type == "scope").Select(c => c.Value).ToList();
        //    if (!scopes.Contains("AdminPermission"))
        //    {
        //        return Unauthorized();
        //    }
        //    var client = _httpClientFactory.CreateClient();
        //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));



        //    var responseMessage = await client.GetAsync("https://localhost:5001/api/GetRoleList/");
        //    if (responseMessage != null)
        //    {
        //        var jsondata = await responseMessage.Content.ReadAsStringAsync();
        //        var userroles = JsonConvert.DeserializeObject<List<RoleAssignViewModel>>(jsondata);
                
        //        var responseMessage2 = await client.GetAsync("https://localhost:5001/api/RoleList/");
        //        if (responseMessage2 != null)
        //        {
        //            var jsondata2 = await responseMessage2.Content.ReadAsStringAsync();
        //            var roles = JsonConvert.DeserializeObject<List<RoleAssignViewModel>>(jsondata2);
        //            List<RoleAssignViewModel> roleAssignViewModels = new List<RoleAssignViewModel>();
        //            foreach (var item in roles)
        //            {
        //                RoleAssignViewModel model = new RoleAssignViewModel();
        //                model.RoleID = item.RoleID;
        //                model.RoleName = item.RoleName;
        //                foreach(var item2 in userroles)
        //                {
        //                    model.RoleExist = item2.RoleName==item.RoleName;
        //                }
                        
        //                roleAssignViewModels.Add(model);
        //            }
        //            TempData["userid"] = id;
        //            return View(roleAssignViewModels);
        //        }
        //    }

        //    return View();
            

        //}

        //[HttpPost]
        //public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> model)
        //{
        //    var userid = (int)TempData["userid"];
        //    var user = _userManager.Users.FirstOrDefault(x => x.Id == userid);
        //    foreach (var item in model)
        //    {
        //        if (item.RoleExist)
        //        {
        //            await _userManager.AddToRoleAsync(user, item.RoleName);
        //        }
        //        else
        //        {
        //            await _userManager.RemoveFromRoleAsync(user, item.RoleName);
        //        }
        //    }
        //    return RedirectToAction("UserList");
        //}

    }
}
