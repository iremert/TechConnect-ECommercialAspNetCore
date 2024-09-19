using Microsoft.AspNetCore.Mvc;

namespace TechConnect.WebUI.Controllers
{
    [Route("erisim-kisiti")]
    public class AccessDeniedController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
