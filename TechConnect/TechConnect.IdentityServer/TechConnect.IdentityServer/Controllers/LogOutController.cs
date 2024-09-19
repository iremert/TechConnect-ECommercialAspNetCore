using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechConnect.IdentityServer.Models;

namespace TechConnect.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogOutController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signinmanager;

        public LogOutController(SignInManager<ApplicationUser> signinmanager)
        {
            _signinmanager = signinmanager;
        }


        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signinmanager.SignOutAsync();
            return Ok();
        }
    }
}
