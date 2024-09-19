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
    public class GetUserRealController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserRealController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {


            var user = _userManager.Users.Where(x => x.Id == id).SingleOrDefault();
            if (user == null)
            {
                return Ok();
            }
            else
            {
                return Ok(user);
            }
        }
    }
}
