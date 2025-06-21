
namespace PruebaTecnica.Service
{
    public interface ITwitchService
    {
        public Task<HttpResponseMessage> GetStreamerById(string id);
        public Task<HttpResponseMessage> GetStreams();
    }
}
