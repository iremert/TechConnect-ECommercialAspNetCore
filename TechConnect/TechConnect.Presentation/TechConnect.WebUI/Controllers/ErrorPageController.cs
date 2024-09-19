using Microsoft.AspNetCore.Mvc;

namespace TechConnect.WebUI.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error404(int code)
        {
            string errorMessage = "";

            switch (code)
            {
                case 404:
                    errorMessage = "Ooops, Aradığın sayfa bulunamadı :( !";
                    break;
                // Diğer durumlar için gerekli kontrolleri ekleyebilirsiniz.
                default:
                    errorMessage = "Bir hata oluştu :( !";
                    break;
            }

            ViewBag.ErrorMessage = errorMessage; // ViewBag ile view'e gönderilen veri

            return View();
        }
    }
}
