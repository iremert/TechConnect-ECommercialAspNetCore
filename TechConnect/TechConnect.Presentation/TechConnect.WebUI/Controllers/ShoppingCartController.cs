using Microsoft.AspNetCore.Mvc;

namespace TechConnect.WebUI.Controllers
{

    [Route("sipariş-sepeti")]
    public class ShoppingCartController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("adres-işlemleri")]
        public IActionResult Index2()
        {
            return View();
        }


        [Route("güvenli-ödeme")]
        public IActionResult Index3()
        {
            return View();
        }
    }
}
