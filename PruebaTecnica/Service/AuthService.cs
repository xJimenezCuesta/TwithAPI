﻿using Newtonsoft.Json;
using PruebaTecnica.Responses;
using System.Net.Http.Headers;

namespace PruebaTecnica.Services
{
    public class AuthService
    {

        const string CLIENT_ID = "dlkwq9i2okmcofq0420dba20reo4uw";
        const string CLIEN_SECRET = "8c0ky0ee4nj92xj8fvk1bq0l8v46lp";

        const string URL_OAUTH = "https://id.twitch.tv/oauth2/token";

        public AuthService() { }

        public async Task<HttpResponseMessage> GetResponse(string url)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Client-ID", CLIENT_ID);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());

            return await client.GetAsync(url);
        }


        private async Task<string> GetAccessToken()
        {
            using HttpClient client = new HttpClient();

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", CLIENT_ID),
                new KeyValuePair<string, string>("client_secret", CLIEN_SECRET),
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
            });

            var response = await client.PostAsync(URL_OAUTH, content);
            string tokenBody = await response.Content.ReadAsStringAsync();

            AuthResponse auth = JsonConvert.DeserializeObject<AuthResponse>(tokenBody);
            return auth.Access_token;
        }

    }
}
