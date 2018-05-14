using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MusicAssistantTgBot.Models
{
    public class Artist
    {
        [JsonProperty("id")]
        public long id { get; set; }

        [JsonProperty("nickName")]
        public string nickName { get; set; }

        [JsonProperty("firstName")]
        public string firstName { get; set; }

        [JsonProperty("lastName")]
        public string lastName { get; set; }

        [JsonProperty("birthDate")]
        public DateTimeOffset birthDate { get; set; }

        [JsonProperty("deathDate")]
        public DateTimeOffset deathDate { get; set; }

        [JsonProperty("careerStart")]
        public DateTimeOffset careerStart { get; set; }

        [JsonProperty("careerEnd")]
        public DateTimeOffset careerEnd { get; set; }

        [JsonProperty("birthPlace")]
        public string birthPlace { get; set; }

        [JsonProperty("gender")]
        public string gender { get; set; }

        [JsonProperty("biography")]
        public string biography { get; set; }

        [JsonProperty("artistPhotoUrl")]
        public string artistPhotoUrl { get; set; }

        [JsonProperty("awards")]
        public List<object> awards { get; set; }

        [JsonProperty("albums")]
        public List<Album> albums { get; set; }
    }
}