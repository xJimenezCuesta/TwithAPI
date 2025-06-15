using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaTecnica.Models;
using PruebaTecnica.Responses;
using System.Net;
using System.Net.Http.Headers;


namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("analytics")]
    public class TwitchAPI : ControllerBase
    {
        const string URL_GET = "https://api.twitch.tv/helix/users?id=";
        const string URL_OAUTH = "https://id.twitch.tv/oauth2/token";
        const string URL_LIVE_STREAMS = "https://api.twitch.tv/helix/streams";

        const string CLIENT_ID = "dlkwq9i2okmcofq0420dba20reo4uw";
        const string CLIEN_SECRET = "8c0ky0ee4nj92xj8fvk1bq0l8v46lp";

        public TwitchAPI() { }

        [HttpGet("user")]
        public async Task<ObjectResult> Get(string id)
        {
            if (!Streamer.ValidId(id))
            {
                return BadRequest(new Excepcion("Invalid or missing 'id' parameter."));
            }
            
            HttpResponseMessage respuesta =  await GetResponse(URL_GET + id);

            if (respuesta.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized(new Excepcion("Unauthorized. Twitch access token is invalid or has expired."));
            }
            else if (!respuesta.IsSuccessStatusCode)
            {
                string errorContent = await respuesta.Content.ReadAsStringAsync();
                return StatusCode(500, new Excepcion("Internal server error."));
            }
            else 
            { 
                var data = await respuesta.Content.ReadAsStringAsync();
                Streamer streamer = JsonConvert.DeserializeObject<GetByIdResponse>(data).Data.FirstOrDefault();
                
                if (streamer == null)
                {
                    return NotFound( new Excepcion("User not found."));
                }
                else
                {
                    return Ok(streamer);
                }   
            }
        }


        [HttpGet("streams")]
        public async Task<ObjectResult> GetLiveStreams()
        {
            HttpResponseMessage respuesta = await GetResponse(URL_LIVE_STREAMS);

            if (respuesta.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized(new Excepcion("Unauthorized. Twitch access token is invalid or has expired."));
            }

            if (respuesta.IsSuccessStatusCode)
            {

                var data = await respuesta.Content.ReadAsStringAsync();
                var streams = JsonConvert.DeserializeObject<GetLiveStreamsResponse>(data);
                return Ok(streams.Data);
            }
            else
            {
                string errorContent = await respuesta.Content.ReadAsStringAsync();
                return StatusCode(500, new Excepcion("Internal server error."));
            }
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

        private async Task<HttpResponseMessage> GetResponse(string url)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Client-ID", CLIENT_ID);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());
            
            return await client.GetAsync(url);
        }

    }
}
