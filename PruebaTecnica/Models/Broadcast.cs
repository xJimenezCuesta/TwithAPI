namespace PruebaTecnica.Models
{
    public class Broadcast
    {
        public string Id { get; set; }
        public string User_id { get; set; }
        public string User_name { get; set; }
        public string Game_id { get; set; }
        public string Game_name { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int Viewer_count { get; set; }
        public DateTime Started_at { get; set; }
        public string Language { get; set; }
        public string Thumbnail_url { get; set; }
        public string[] Tag_ids { get; set; }
        public string[] Tags { get; set; }
        public bool Is_mature { get; set; }

    }
}
