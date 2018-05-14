using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MusicAssistantTgBot.Models
{
    public class Album
    {
        [JsonProperty("id")]
        public long id { get; set; }

        [JsonProperty("artist", NullValueHandling = NullValueHandling.Ignore)]
        public Artist artist { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("genre")]
        public string genre { get; set; }

        [JsonProperty("releaseDate")]
        public DateTimeOffset releaseDate { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("albumCoverUrl")]
        public string albumCoverUrl { get; set; }

        [JsonProperty("tracks")]
        public List<Track> tracks { get; set; }
    }
}