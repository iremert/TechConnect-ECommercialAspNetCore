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
    public class RegistersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterDto userRegisterDto)
        {

            var value = await _userManager.FindByEmailAsync(userRegisterDto.Email);
            if (value == null)
            {
                var values = new ApplicationUser()
                {
                   
                    UserName = userRegisterDto.UserName,
                    Email = userRegisterDto.Email,
                    Name = userRegisterDto.Name,
                    Surname = userRegisterDto.Surname,
                };
                var result = await _userManager.CreateAsync(values, userRegisterDto.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(values, "User");
                    return Ok("Kullanıcı başarıyla eklendi.");
                }
                else
                {
                    return BadRequest("Bir hata oluştu tekrar deneyiniz.");
                }
            }
            else
            {
                return BadRequest("Bu mail önceden kullanılmış, lütfen yeniden deneyin.");
            }
        }
    }
}
