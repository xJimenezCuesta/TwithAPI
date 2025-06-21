using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Controllers;
using PruebaTecnica.Service;

namespace TestProject
{
    public class GetLiveStreamsTest
    {
        private TwitchAPI _twitchAPI;

        [SetUp]
        public void Setup()
        {
            _twitchAPI = new TwitchAPI(new TwitchService());
        }

        [Test]
        public async Task Respuesta_200_cuando_llamada_correcta()
        {
            ObjectResult respuesta = await _twitchAPI.GetLiveStreams();
            var algo = respuesta.Value.GetType();
            Assert.AreEqual(respuesta.StatusCode, 200);
        }
    }
}
