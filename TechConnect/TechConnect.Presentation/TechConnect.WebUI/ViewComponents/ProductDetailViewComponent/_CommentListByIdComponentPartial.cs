using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechConnect.DtoUI.CommentDtos;
using TechConnect.WebUI.Models;

namespace TechConnect.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _CommentListByIdComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CommentListByIdComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));



            var request = await client.GetAsync("https://localhost:7237/api/Comment/");
            if (request.IsSuccessStatusCode)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(responseMessage);
                values = values.Where(x => x.ProductId ==id  && x.Status == true).ToList();
                foreach (var value in values)
                {
                    var responseMessage2 = await client.GetAsync("https://localhost:5001/api/GetUserReal/" + value.UserId);
                    if (responseMessage2 != null)
                    {
                        var jsondata = await responseMessage2.Content.ReadAsStringAsync();
                        var uservalues = JsonConvert.DeserializeObject<_GetUserRealValue>(jsondata);
                        if (uservalues != null)
                        {
                            value.UserNameSurname = uservalues.Name + " " + uservalues.Surname;
                        }
                    }
                   
                }
                return View(values);
            }
            return View();
        }
    }
}
