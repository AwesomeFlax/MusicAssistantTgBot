namespace MusicAssistantTgBot.Models
{
    public class Song
    {
        public int id { get; set; }
        public Album album { get; set; }
        public string name { get; set; }
        public string lyrics { get; set; }
        public object userCollection { get; set; }
    }
}