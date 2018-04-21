using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace MusicAssistantTgBot
{
    public class Management
    {
        protected static readonly InlineKeyboardMarkup Inline = new InlineKeyboardMarkup(GetInlineKeyboard(new[] { Previous, Next }));
        protected static int SearchArea;
        protected const string BaseLink = "http://musicrestwebapi.azurewebsites.net/";

        #region Commands strings
        protected const string Start = "/start";
        protected const string Back = "Go back to main menu";
        protected const string Help = "/help";
        protected const string SearchForMusic = "Search for music";
        protected const string SearchForAlbums = "Search for albums";
        protected const string SearchForArtists = "Search for artists";
        protected const string GetRandomMusic = "Get some good music";
        private const string Next = "➡️";
        private const string Previous = "⬅️";
        #endregion

        private static InlineKeyboardButton[][] GetInlineKeyboard(string[] stringArray)
        {
            var keyboardInline = new InlineKeyboardButton[1][];
            var keyboardButtons = new InlineKeyboardButton[stringArray.Length];
            for (var i = 0; i < stringArray.Length; i++)
            {
                keyboardButtons[i] = new InlineKeyboardButton
                {
                    Text = stringArray[i],
                    CallbackData = stringArray[i]
                };
            }
            keyboardInline[0] = keyboardButtons;
            return keyboardInline;
        }


        public static void GetCommand(string command, TelegramBotClient telegramBot, long cid, int mid)
        {
            switch (command)
            {
                case Start:
                    Commands.Command_Start(telegramBot, cid);
                    break;

                case Back:
                    Commands.Command_Back(telegramBot, cid);
                    break;

                case SearchForMusic:
                    Commands.Command_SearchForMusic(telegramBot, cid);
                    break;

                case SearchForAlbums:
                    Commands.Command_SearchForAlbum(telegramBot, cid);
                    break;

                case SearchForArtists:
                    Commands.Command_SearchForArtist(telegramBot, cid);
                    break;
            }
        }

        public static void GetParametres(string extension, TelegramBotClient telegramBot, long cid, int mid)
        {
            switch (SearchArea)
            {
                case 1:
                    Response.Response_Song(telegramBot, cid, mid, extension);
                    break;

                case 2:
                    Response.Response_Album(telegramBot, cid, extension);
                    break;

                case 3:
                    Response.Response_Artist(telegramBot, cid, extension);
                    break;

                default:
                    telegramBot.SendTextMessageAsync
                        (cid, "Beeing honest - have no idea what are you talking about 🤔");
                    break;
            }
        }

        public static void GetInline(string command, TelegramBotClient telegramBot, long cid, int mid)
        {
            switch (command)
            {
                case Next:
                    MusicAssistantTgBot.Inline.Inline_Next(command, telegramBot, cid, mid);
                    break;

                case Previous:
                    MusicAssistantTgBot.Inline.Inline_Previous(command, telegramBot, cid, mid);
                    break;
            }
        }
    }
}
