using PruebaTecnica.Models;

namespace PruebaTecnica.Responses
{
    public class GetLiveStreamsResponse
    {
        public Broadcast[] Data { get; set; }
        public Pagination Pagination { get; set; }
    }
}
