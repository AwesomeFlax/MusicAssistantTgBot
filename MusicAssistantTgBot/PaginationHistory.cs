using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicAssistantTgBot
{
    public sealed class PaginationHistory
    {
        private static PaginationHistory Instance;

        List<HistoryObject> historyObjects;

        private PaginationHistory()
        {
            historyObjects = new List<HistoryObject>();
        }

        public static PaginationHistory getInstance()
        {
            if (Instance == null)
            {
                Instance = new PaginationHistory();
            }

            return Instance;
        }

        public List<string> GetByMessageId(int MId)
        {
            try
            {
                return historyObjects.Single(x => x.MId == MId).SearchResults;
            }
            catch(Exception ex)
            {
                Console.WriteLine("We lost specified message search history :(");
                return new List<string>();
            }
        }

        public void AddInList(HistoryObject historyObject)
        {
            historyObjects.Add(historyObject);
        }
    }
}