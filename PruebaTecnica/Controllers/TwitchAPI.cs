using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaTecnica.Models;
using PruebaTecnica.Responses;
using PruebaTecnica.Services;
using PruebaTecnica.Utils;
using System.Net;


namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("analytics")]
    public class TwitchAPI : ControllerBase
    {
        const string URL_GET = "https://api.twitch.tv/helix/users?id=";
        const string URL_LIVE_STREAMS = "https://api.twitch.tv/helix/streams";

        private readonly AuthService _authService;

        public TwitchAPI()
        { 
            _authService = new AuthService();
        }

        [HttpGet("user")]
        public async Task<ObjectResult> Get(string id)
        {
            if (!FormatValidator.ValidateId(id))
            {
                return BadRequest(new Excepcion("Invalid or missing 'id' parameter."));
            }
            
            HttpResponseMessage respuesta =  await _authService.GetResponse(URL_GET + id);

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
            HttpResponseMessage respuesta = await _authService.GetResponse(URL_LIVE_STREAMS);

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

    }
}
