using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Linq;
using System.Threading.Tasks;
using TechConnect.IdentityServer.Models;

namespace TechConnect.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetRoleListController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public GetRoleListController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(string id)
        {
            var user = _userManager.Users.Where(x => x.Id == id).SingleOrDefault();
            var role =await  _userManager.GetRolesAsync(user);
            if (role != null)
            {
                return Ok(role);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
