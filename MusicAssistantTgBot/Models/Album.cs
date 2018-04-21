using System;
using System.Collections.Generic;

namespace MusicAssistantTgBot.Models
{
    public class Album
    {
        public object albumCover { get; set; }
        public int id { get; set; }
        public Artist artist { get; set; }
        public string name { get; set; }
        public string genre { get; set; }
        public DateTime releaseDate { get; set; }
        public string description { get; set; }
        public string albumCoverUrl { get; set; }
        public List<Song> tracks { get; set; }
    }
}