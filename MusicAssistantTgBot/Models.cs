using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAssistantTgBot
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

    public class Song
    {
        public int id { get; set; }
        public Album album { get; set; }
        public string name { get; set; }
        public string lyrics { get; set; }
        public object userCollection { get; set; }
    }

    public class Award
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime rewardDate { get; set; }
    }

    public class Artist
    {
        public object artistPhoto { get; set; }
        public int id { get; set; }
        public string nickName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthDate { get; set; }
        public DateTime? deathDate { get; set; }
        public DateTime careerStart { get; set; }
        public DateTime? careerEnd { get; set; }
        public string birthPlace { get; set; }
        public string gender { get; set; }
        public string biography { get; set; }
        public string artistPhotoUrl { get; set; }
        public List<Award> awards { get; set; }
        public List<Album> albums { get; set; }
    }
}
