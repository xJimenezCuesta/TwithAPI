using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Controllers;
using PruebaTecnica.Service;

namespace TestProject
{
    public class GetUserByIdTest
    {
        private TwitchAPI _twitchAPI;

        [SetUp]
        public void Setup()
        {
            _twitchAPI = new TwitchAPI(new TwitchService());
        }

        [Test]
        public async Task Respuesta_400_cuando_id_null()
        {
            string id = null;
            ObjectResult respuesta = await _twitchAPI.Get(id);
            Assert.AreEqual(respuesta.StatusCode, 400);
        }

        [Test]
        public async Task Respuesta_400_cuando_id_igual_0()
        {
            string id = "0";
            ObjectResult respuesta = await _twitchAPI.Get(id);
            Assert.AreEqual(respuesta.StatusCode, 400);
        }

        [Test]
        public async Task Respuesta_400_cuando_id_contiene_letras()
        {
            string id = "abcde";
            ObjectResult respuesta = await _twitchAPI.Get(id);
            Assert.AreEqual(respuesta.StatusCode, 400);
        }

        [Test]
        public async Task Respuesta_400_cuando_id_mas_de_10_digitos()
        {
            string id = "01234567890";
            ObjectResult respuesta = await _twitchAPI.Get(id);
            Assert.AreEqual(respuesta.StatusCode, 400);
        }

        [Test]
        public async Task Respuesta_402_cuando_usuario_no_existe()
        {
            string id = "123123";
            ObjectResult respuesta = await _twitchAPI.Get(id);
            Assert.AreEqual(respuesta.StatusCode, 404);
        }

        [Test]
        public async Task Respuesta_200_cuando_usuario_existe()
        {
            string id = "123456789";
            ObjectResult respuesta = await _twitchAPI.Get(id);
            Assert.AreEqual(respuesta.StatusCode, 200);
        }

    }
}