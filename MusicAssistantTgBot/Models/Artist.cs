using System;
using System.Collections.Generic;

namespace MusicAssistantTgBot.Models
{
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