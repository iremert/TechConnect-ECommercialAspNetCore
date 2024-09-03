using Microsoft.AspNetCore.Mvc;

namespace TechConnect.WebUI.ViewComponents.AboutViewComponent
{
    public class _CounterComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
