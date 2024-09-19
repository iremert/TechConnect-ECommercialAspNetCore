using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechConnect.DtoUI.DiscountDtos;

namespace TechConnect.WebUI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DiscountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDiscountCoupon()
        {
            return PartialView();
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmDiscountCoupon(string code)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));


            var request = await client.GetAsync("https://localhost:7237/api/Discount/");
            var responseMessage2 = await request.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultDiscountDto>>(responseMessage2);
            var value = "";
            foreach(var item in values)
            {
                if(item.Code==code)
                {
                    value = item.ID;
                }
            }
            
            return RedirectToAction("", "sipariş-sepeti", new { discountid  =value});
        }
    }
}
