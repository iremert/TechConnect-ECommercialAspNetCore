using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace TechConnect.WebUI.Services.Concrete
{
    public static class CreateVisitorToken
    {
        public static async Task AddAuthorizationHeaderAsync(this HttpClient client, IHttpClientFactory httpClientFactory)
        {
            string token = await GetTokenAsync(httpClientFactory);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static async Task<string> GetTokenAsync(IHttpClientFactory httpClientFactory)
        {
            using (var httpClient = httpClientFactory.CreateClient())
            {
                
                // Eğer User tokenı alınamazsa, Visitor olarak kabul edip visitor için token alınıyor.
                var visitorRequest = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://localhost:5001/connect/token"), // token alma endpointi
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"client_id","TechConnectVisitorId" }, // Visitor client id
                        {"client_secret","techconnectsecret" }, // Visitor client secret
                        {"grant_type","client_credentials" }
                    })
                };

                var visitorResponse = await httpClient.SendAsync(visitorRequest);

                if (visitorResponse.IsSuccessStatusCode)
                {
                    var visitorContent = await visitorResponse.Content.ReadAsStringAsync();
                    var visitorTokenResponse = JObject.Parse(visitorContent);
                    return visitorTokenResponse["access_token"].ToString();
                }

                throw new Exception("Token alımı başarısız oldu.");
            }
        }
    }
}
