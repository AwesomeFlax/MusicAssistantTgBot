using Newtonsoft.Json;

namespace MusicAssistantTgBot.Models
{
    public class Track
    {
        [JsonProperty("id")]
        public long id { get; set; }

        [JsonProperty("album", NullValueHandling = NullValueHandling.Ignore)]
        public Album album { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("lyrics")]
        public string lyrics { get; set; }
    }
}