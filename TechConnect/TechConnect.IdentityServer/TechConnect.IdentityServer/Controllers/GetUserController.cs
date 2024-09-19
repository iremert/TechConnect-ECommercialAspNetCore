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
    public class GetUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {


            var user=_userManager.Users.Where(x=>x.Id==id).SingleOrDefault();
            user.Name = "John";
            user.Surname = "Doe";
            user.Email = "email@email.com";
            user.PhoneNumber = "+905350000000";
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
