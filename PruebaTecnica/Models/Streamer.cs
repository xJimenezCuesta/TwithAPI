using System.Text.RegularExpressions;

namespace PruebaTecnica.Models
{

    public class Streamer
    {
        public string Id { get; set; }

        public string Login { get; set; }

        public string Display_name { get; set; }

        public string Type { get; set; }

        public string Broadcaster_type { get; set; }

        public string Description { get; set; }

        public string Profile_image_url { get; set; }

        public string Offline_image_url { get; set; }

        public int View_count { get; set; }

        public DateTime Created_at { get; set; }


        private const string ID_FORMAT = "^[0-9]{1,10}$";

        public static bool ValidId(string id)
        {
            if (string.IsNullOrEmpty(id) || id == "0")
            {
                return false;
            }

            var esValido =  Regex.IsMatch(id, ID_FORMAT);
            return esValido;
        }
    }
}
