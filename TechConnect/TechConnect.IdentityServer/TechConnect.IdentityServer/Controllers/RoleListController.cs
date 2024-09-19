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
    public class RoleListController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleListController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetRole()
        {
            var role = _roleManager.Roles.ToList();
            if(role!=null)
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
