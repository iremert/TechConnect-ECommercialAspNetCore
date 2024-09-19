using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechConnect.IdentityServer.Dtos;
using TechConnect.IdentityServer.Models;

namespace TechConnect.IdentityServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RoleControlsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleControlsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> RoleControls(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.Username);
            if (user == null)
            {
                return BadRequest();
            }

            // 2. Kullanıcının rollerini sorguluyoruz.
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Any())
            {
                return BadRequest();
            }

            return Ok(roles);

        }
    }
}
