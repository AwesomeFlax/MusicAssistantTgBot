using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAssistantTgBot
{
    public class HistoryObject
    {
        public long CId { get; set; }
        public int MId { get; set; }
        public List<string> SearchResults { get; set; }

        public HistoryObject(long _CId, int _MId, List<string> _SearchResults)
        {
            CId = _CId;
            MId = _MId;
            SearchResults = _SearchResults;
        }
    }
}
