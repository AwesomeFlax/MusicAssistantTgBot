using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MusicAssistantTgBot
{
    public class Inline : Management
    {
        public static async void Inline_Next(string command, ITelegramBotClient telegramBot, long cid, int mid)
        {
            await telegramBot.EditMessageTextAsync(cid, mid, "updated next", ParseMode.Default, false, Inline);
        }

        public static async void Inline_Previous(string command, ITelegramBotClient telegramBot, long cid, int mid)
        {
            await telegramBot.EditMessageTextAsync(cid, mid, "updated prev", ParseMode.Default, false, Inline);
        }
    }
}