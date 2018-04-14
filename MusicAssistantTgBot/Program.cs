using System;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MusicAssistantTgBot
{
    class Program
    {
        static string token = @"559385508:AAF7DD82jmTyI2UfA8osYnrWGtmVtjXwmYE";
        static TelegramBotClient telegramBot = new TelegramBotClient(token);

        static void Main(string[] args)
        {
            Console.WriteLine("Bot is ready to help you");

            telegramBot.StartReceiving();
            telegramBot.OnMessage += TelegramBot_OnMessage;
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

                Console.WriteLine("{1} - {2} : {3}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), uid, name, txt);

                if (Commands.CheckIfMessageIsCommand(txt))
                {
                    Commands.GetCommand(txt, telegramBot, cid);
                }
                else
                {
                    Commands.GetParametres(txt, telegramBot, cid);
                }
            }
        }
    }
}