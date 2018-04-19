using System;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MusicAssistantTgBot
{
    public static class Program
    {
        private const string Token = @"559385508:AAF7DD82jmTyI2UfA8osYnrWGtmVtjXwmYE";
        private static readonly TelegramBotClient TelegramBot = new TelegramBotClient(Token);

        private static void Main()
        {
            Console.WriteLine("Bot is ready to help you");

            TelegramBot.StartReceiving();
            TelegramBot.OnMessage += TelegramBot_OnMessage;
            TelegramBot.OnCallbackQuery += TelegramBot_OnCallbackQuery;
            Console.ReadLine();
        }

        private static void TelegramBot_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CallbackQuery.Data))
            {
                var cid = e.CallbackQuery.Message.Chat.Id;
                var data = e.CallbackQuery.Data;
                var mid = e.CallbackQuery.Message.MessageId;

                Console.WriteLine("Inline button has been pressed at " + mid);

                Commands.GetInline(data, TelegramBot, cid, mid);
            }
        }

        private static void TelegramBot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.Text)
            {
                var txt = e.Message.Text;
                var cid = e.Message.Chat.Id;
                var name = e.Message.From.FirstName + " " + e.Message.From.LastName;
                var uid = e.Message.From.Id;
                var mid = e.Message.MessageId;

                Console.WriteLine("{0} - {1} : {2}", uid, name, txt);

                if (Commands.CheckIfMessageIsCommand(txt))
                {
                    Commands.GetCommand(txt, TelegramBot, cid, mid);
                }
                else
                {
                    Commands.GetParametres(txt, TelegramBot, cid, mid);
                }
            }
        }
    }
}