using System;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MusicAssistantTgBot
{
    public class Inline : Management
    {
        public static async void Inline_Next(ITelegramBotClient telegramBot, long cid, int mid, string text)
        {
            var pagesInfo = text.Substring(text.IndexOf('[') + 1, text.IndexOf(']') - 1);
            var parts = pagesInfo.Split('/');

            try
            {
                if (parts[0] != parts[1])
                {
                    int nextIndex = int.Parse(parts[0]) + 1;
                    int quantity = int.Parse(parts[1]);

                    var nextResult = PaginationHistory.getInstance().GetByMessageId(mid).ToArray();
                    string message = "<b>[" + nextIndex + "/" + quantity + "] </b>" + nextResult[nextIndex - 1];

                    await telegramBot.EditMessageTextAsync(cid, mid, message, ParseMode.Html, false, Inline);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(cid + " clicked to fast at 'next arrow': " + ex.Message);
            }
        }

        public static async void Inline_Previous(ITelegramBotClient telegramBot, long cid, int mid, string text)
        {
            var pagesInfo = text.Substring(text.IndexOf('[') + 1, text.IndexOf(']') - 1);
            var parts = pagesInfo.Split('/');

            try
            {
                if (parts[0] != "1")
                {
                    int prevIndex = int.Parse(parts[0]) - 1;
                    int quantity = int.Parse(parts[1]);

                    var nextResult = PaginationHistory.getInstance().GetByMessageId(mid).ToArray();
                    string message = "<b>[" + prevIndex + "/" + quantity + "] </b>" + nextResult[prevIndex - 1];

                    await telegramBot.EditMessageTextAsync(cid, mid, message, ParseMode.Html, false, Inline);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(cid + " clicked to fast at 'prev arrow': " + ex.Message);
            }
        }

        public static async void Inline_Refresh(TelegramBotClient telegramBot, long cid, int mid, string text)
        {

        }
    }
}