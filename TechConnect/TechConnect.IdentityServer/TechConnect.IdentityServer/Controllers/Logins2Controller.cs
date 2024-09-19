using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechConnect.IdentityServer.Dtos;
using TechConnect.IdentityServer.Models;

namespace TechConnect.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Logins2Controller : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signinmanager;

        public Logins2Controller(SignInManager<ApplicationUser> signinmanager)
        {
            _signinmanager = signinmanager;
        }


        [HttpPost]
        public async Task<IActionResult> UserLogin2(UserLoginDto userLoginDto)
        {
            var result = await _signinmanager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, false, false);
           if(result.Succeeded)
            {
                return Ok();
            }
           else
            {
                return BadRequest();
            }
        }
    }
}
