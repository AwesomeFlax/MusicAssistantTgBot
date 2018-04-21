namespace MusicAssistantTgBot
{
    public class _SearchArea
    {
        public int area { get; set; }
        public long chatId { get; set; }

        public _SearchArea(int _area, long _chatId)
        {
            area = _area;
            chatId = _chatId;
        }
    }
}