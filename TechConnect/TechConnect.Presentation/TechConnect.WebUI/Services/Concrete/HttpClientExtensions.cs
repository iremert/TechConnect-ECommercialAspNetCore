namespace TechConnect.WebUI.Services.Concrete
{
    using Newtonsoft.Json.Linq;
    using System.Net.Http.Headers;
    using TechConnect.DtoUI.IdentityDtos.LoginDtos;

    public static class HttpClientExtensions
    {
        public static async Task AddAuthorizationHeaderAsync(this HttpClient client, IHttpClientFactory httpClientFactory,SignInDto signInDto)
        {
            string token = await GetTokenAsync(httpClientFactory, signInDto);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static async Task<string> GetTokenAsync(IHttpClientFactory httpClientFactory,SignInDto signInDto)
        {
            using (var httpClient = httpClientFactory.CreateClient())
            {
                // Kullanıcının rolünü belirlemek için kimlik doğrulama isteği yapıyoruz.
                var userRequest = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://localhost:5001/connect/token"), // token alma endpointi
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"client_id","TechConnectUserId" }, // User client id
                        {"client_secret","techconnectsecret" }, // User client secret
                        {"grant_type","password" },
                        {"username", signInDto.Username },
                        {"password", signInDto.Password }
                    })
                };
                var userResponse = await httpClient.SendAsync(userRequest);
                if (userResponse.IsSuccessStatusCode)
                {
                    var content = await userResponse.Content.ReadAsStringAsync();
                    var tokenResponse = JObject.Parse(content);
                    return tokenResponse["access_token"].ToString();
                }






                var userRequest2 = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://localhost:5001/connect/token"),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"client_id","TechConnectAdminId" },
                        {"client_secret","techconnectsecret" }, 
                        {"grant_type","password" },
                        {"username", signInDto.Username },
                        {"password", signInDto.Password }
                    })
                };
                var userResponse2 = await httpClient.SendAsync(userRequest2);
                if (userResponse2.IsSuccessStatusCode)
                {
                    var content = await userResponse2.Content.ReadAsStringAsync();
                    var tokenResponse = JObject.Parse(content);
                    return tokenResponse["access_token"].ToString();
                }






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
