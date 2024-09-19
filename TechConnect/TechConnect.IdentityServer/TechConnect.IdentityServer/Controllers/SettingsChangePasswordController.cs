using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechConnect.IdentityServer.Dtos;
using TechConnect.IdentityServer.Models;

namespace TechConnect.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsChangePasswordController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SettingsChangePasswordController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }



        [HttpPost]
        public async Task<IActionResult> Settings(SettingsUser settingsUser)
        {
            var user = _userManager.Users.Where(x => x.Id == settingsUser.UserId).SingleOrDefault();

            //burası snaki eksik

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, settingsUser.Password);
            var result = await _userManager.UpdateAsync(user);//update ettik
            if (result.Succeeded)
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
