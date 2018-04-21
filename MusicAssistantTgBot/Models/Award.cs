using System;

namespace MusicAssistantTgBot.Models
{
    public class Award
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime rewardDate { get; set; }
    }
}