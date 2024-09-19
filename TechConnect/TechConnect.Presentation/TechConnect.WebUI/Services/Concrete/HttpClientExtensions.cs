namespace TechConnect.WebUI.Services.Concrete
{
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text;
    using TechConnect.DtoUI.IdentityDtos.LoginDtos;

    public static class HttpClientExtensions
    {
        public static async Task AddAuthorizationHeaderAsync(this HttpClient client, IHttpClientFactory httpClientFactory, SignInDto signInDto)
        {
            string token = await GetTokenAsync(httpClientFactory, signInDto);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static async Task<string> GetTokenAsync(IHttpClientFactory httpClientFactory, SignInDto signInDto)
        {
            using (var httpClient = httpClientFactory.CreateClient())
            {
                var jsonData2 = JsonConvert.SerializeObject(signInDto);
                StringContent stringContent2 = new StringContent(jsonData2, Encoding.UTF8, "application/json");
                var tokenResponse2 = await httpClient.PostAsync("https://localhost:5001/api/RoleControls", stringContent2);
                if (tokenResponse2.IsSuccessStatusCode)
                {
                    var deger = tokenResponse2.Content.ReadAsStringAsync().Result;

                    string clientId = null, clientSecret = null, role = null;

                    if (deger.Contains("Admin"))
                    {
                        clientId = "TechConnectAdminId";
                        clientSecret = "techconnectsecret"; // Admin client secret
                        role = "Admin";
                    }
                    else if (deger.Contains("User"))
                    {
                        clientId = "TechConnectUserId";
                        clientSecret = "techconnectsecret"; // User client secret
                        role = "User";
                    }
                    else
                    {
                        clientId = "TechConnectVisitorId";
                        clientSecret = "techconnectsecret";
                        role = "Visitor";
                        var visitorRequest = new HttpRequestMessage
                        {
                            RequestUri = new Uri("https://localhost:5001/connect/token"), // token alma endpointi
                            Method = HttpMethod.Post,
                            Content = new FormUrlEncodedContent(new Dictionary<string, string>
                            {
                                {"client_id",clientId}, // Visitor client id
                                {"client_secret",clientSecret}, // Visitor client secret
                                {"grant_type","client_credentials" }
                            })
                        };

                        var visitorResponse = await httpClient.SendAsync(visitorRequest);

                        if (visitorResponse.IsSuccessStatusCode)
                        {
                            var tokenContent = await visitorResponse.Content.ReadAsStringAsync();
                            var tokenResult = JObject.Parse(tokenContent);

                            return tokenResult["access_token"].ToString(); // Token döndürülüyor.

                        }
                    }


                    var tokenRequest = new HttpRequestMessage
                    {
                        RequestUri = new Uri("https://localhost:5001/connect/token"),
                        Method = HttpMethod.Post,
                        Content = new FormUrlEncodedContent(new Dictionary<string, string>
                        {
                            {"client_id", clientId },
                            {"client_secret", clientSecret },
                            {"grant_type", "password" },
                            {"username", signInDto.Username },
                            {"password", signInDto.Password }
                        })
                    };


                    var tokenResponse = await httpClient.SendAsync(tokenRequest);
                    if (tokenResponse.IsSuccessStatusCode)
                    {

                        var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
                        var tokenResult = JObject.Parse(tokenContent);

                        return tokenResult["access_token"].ToString(); // Token döndürülüyor.
                    }
                }

                throw new Exception("Token alımı başarısız oldu.");
            }
        }
    }

}
