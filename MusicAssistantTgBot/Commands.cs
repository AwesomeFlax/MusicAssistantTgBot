using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MusicAssistantTgBot
{
    public class Commands : Management
    {
        private static readonly ReplyKeyboardMarkup Markup = new ReplyKeyboardMarkup();
        
        static readonly string[] commands =
        {
            Start, Back, Help, SearchForAlbums, SearchForArtists, SearchForMusic, GetRandomMusic
        };

        private static readonly List<string> CommandList = new List<string>(commands);

        public static bool CheckIfMessageIsCommand(string command)
        {
            return CommandList.Contains(command);
        }
        
        public static async void Command_Start(ITelegramBotClient telegramBot, long cid)
        {
            SearchArea = 0;

            Markup.ResizeKeyboard = true;
            Markup.Keyboard = new[]
            {
                                new[]
                                {
                                    new KeyboardButton(SearchForMusic),
                                    new KeyboardButton(SearchForAlbums)
                                },
                                new[]
                                {
                                    new KeyboardButton(SearchForArtists),
                                    new KeyboardButton(GetRandomMusic)
                                }
            };

            await telegramBot.SendTextMessageAsync(cid, "Ok, lets do this 😈",
                ParseMode.Default, false, false, 0, Markup);
        }

        public static async void Command_Back(ITelegramBotClient telegramBot, long cid)
        {
            SearchArea = 0;

            Markup.ResizeKeyboard = true;
            Markup.Keyboard = new[]
            {
                                new[]
                                {
                                    new KeyboardButton(SearchForMusic),
                                    new KeyboardButton(SearchForAlbums)
                                },
                                new[]
                                {
                                    new KeyboardButton(SearchForArtists),
                                    new KeyboardButton(GetRandomMusic)
                                }
            };

            await telegramBot.SendTextMessageAsync(cid, "I'm ready to serve you 😉",
               ParseMode.Default, false, false, 0, Markup);
        }

        public static async void Command_SearchForMusic(ITelegramBotClient telegramBot, long cid)
        {
            SearchArea = 1;

            #region markup settings
            Markup.ResizeKeyboard = true;
            Markup.Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton(Back)
                }
            };
            #endregion

            await telegramBot.SendTextMessageAsync(cid, "Enter song name, sir! 🎵",
                ParseMode.Default, false, false, 0, Markup);
        }

        public static async void Command_SearchForAlbum(ITelegramBotClient telegramBot, long cid)
        {
            SearchArea = 2;

            #region markup settings
            Markup.ResizeKeyboard = true;
            Markup.Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton(Back)
                }
            };
            #endregion

            await telegramBot.SendTextMessageAsync(cid, "Enter album name, sir! 📓",
                ParseMode.Default, false, false, 0, Markup);
        }

        public static async void Command_SearchForArtist(ITelegramBotClient telegramBot, long cid)
        {
            SearchArea = 3;

            #region markup settings
            Markup.ResizeKeyboard = true;
            Markup.Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton(Back)
                }
            };
            #endregion

            await telegramBot.SendTextMessageAsync(cid, "Enter artist name, sir! 💃🏼",
                ParseMode.Default, false, false, 0, Markup);
        }
    }
}