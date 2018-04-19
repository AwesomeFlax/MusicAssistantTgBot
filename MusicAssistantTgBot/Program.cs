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
            Console.ReadLine();
        }

        private static void TelegramBot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.Text)
            {
                var txt = e.Message.Text;
                var cid = e.Message.Chat.Id;
                var name = e.Message.From.FirstName + " " + e.Message.From.LastName;
                var uid = e.Message.From.Id;

                Console.WriteLine("{0} - {1} : {2}", uid, name, txt);

                if (Commands.CheckIfMessageIsCommand(txt))
                {
                    Commands.GetCommand(txt, TelegramBot, cid);
                }
                else
                {
                    Commands.GetParametres(txt, TelegramBot, cid);
                }
            }
        }
    }
}