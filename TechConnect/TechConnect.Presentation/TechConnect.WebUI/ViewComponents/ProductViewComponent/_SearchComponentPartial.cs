using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechConnect.DtoUI.ProductDto;

namespace TechConnect.WebUI.ViewComponents.ProductViewComponent
{
    public class _SearchComponentPartial:ViewComponent
    {
        
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }
    }
}
