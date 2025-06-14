using System.Text.RegularExpressions;

namespace PruebaTecnica.Models
{
    public class Streamer
    {
        public string Broadcaster_id { get; set; }

        public string Broadcaster_login { get; set; }
        public string Broadcaster_name { get; set; }
        public string Broadcaster_language { get; set; }
        public string Game_id { get; set; }
        public string Game_name { get; set; }
        public string Title { get; set; }
        public uint Delay { get; set; }
        public string[] Tags { get; set; }
        public string[] Content_classification_labels { get; set; }
        public bool Is_branded_content { get; set; }

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
