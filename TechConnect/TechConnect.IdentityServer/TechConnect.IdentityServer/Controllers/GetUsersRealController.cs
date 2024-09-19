using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TechConnect.IdentityServer.Models;

namespace TechConnect.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUsersRealController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUsersRealController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {


            var user = _userManager.Users.ToList();
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(user);
            }
        }
    }
}
