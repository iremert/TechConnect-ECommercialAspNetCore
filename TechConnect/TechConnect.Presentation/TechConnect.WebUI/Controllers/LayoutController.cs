using Microsoft.AspNetCore.Mvc;

namespace TechConnect.WebUI.Controllers
{
    public class LayoutController : Controller
    {
        public IActionResult _Layout()
        {
            return View();
        }
    }
}
