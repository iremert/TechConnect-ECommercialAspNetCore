using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechConnect.DtoUI.BasketTotalDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.WebUI.Controllers
{
    public class LayoutController : Controller
    {
        

        public async Task<IActionResult> _Layout()
        {
           
            return View();
        }



        public async Task<IActionResult> _AdminLayout()
        {

            return View();
        }
    }
}
