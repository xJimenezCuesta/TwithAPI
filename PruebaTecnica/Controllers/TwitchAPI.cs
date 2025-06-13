using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaTecnica.Models;
using PruebaTecnica.Responses;
using System.Net.Http.Headers;


namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("analytics")]
    public class TwitchAPI : ControllerBase
    {
        const string URL_GET = "https://api.twitch.tv/helix/channels?broadcaster_id=";
        const string URL_OAUTH = "https://id.twitch.tv/oauth2/token";
        const string URL_LIVE_STREAMS = "https://api.twitch.tv/helix/streams";

        const string CLIENT_ID = "dlkwq9i2okmcofq0420dba20reo4uw";
        const string CLIEN_SECRET = "8c0ky0ee4nj92xj8fvk1bq0l8v46lp";

        public TwitchAPI() { }

        [HttpGet("user")]
        public async Task<Streamer> Get(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                AuthResponse auth = await GetAccessToken();

                client.DefaultRequestHeaders.Add("Client-ID", CLIENT_ID);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.Access_token);

                HttpResponseMessage respuesta = await client.GetAsync(URL_GET + id);
                if (respuesta.IsSuccessStatusCode)
                {
                    var data = await respuesta.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<GetByIdResponse>(data).Data.FirstOrDefault();
                }
                else
                {
                    string errorContent = await respuesta.Content.ReadAsStringAsync();
                    return null;
                }
            }
        }

        [HttpGet("streams")]
        public async Task<GetLiveStreamsResponse> GetLiveStreams()
        {
            using (HttpClient client = new HttpClient())
            {
                AuthResponse auth = await GetAccessToken();

                client.DefaultRequestHeaders.Add("Client-ID", CLIENT_ID);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.Access_token);

                HttpResponseMessage respuesta = await client.GetAsync(URL_LIVE_STREAMS); 
                var data = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GetLiveStreamsResponse>(data);
            }
        }

        private async Task<AuthResponse> GetAccessToken()
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", CLIENT_ID),
                    new KeyValuePair<string, string>("client_secret", CLIEN_SECRET),
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                });

                var response = await client.PostAsync(URL_OAUTH, content);
                string tokenBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<AuthResponse>(tokenBody);
            }
        }

       
    }
}
