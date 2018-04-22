using System;
using System.Collections.Generic;
using System.Linq;
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
            #region SearchAreaSession
            if (SearchArea.All(x => x.chatId != cid))
            {
                Console.WriteLine("For someone in ChatId " + cid + " new session has been created");
                SearchArea.Add(new _SearchArea(0, cid));
            }
            else
            {
                SearchArea.Single(x => x.chatId == cid).area = 0;
            }
            #endregion

            #region markup settings
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
            #endregion

            await telegramBot.SendTextMessageAsync(cid, "Ok, lets do this 😈",
                ParseMode.Default, false, false, 0, Markup);
        }

        public static async void Command_Back(ITelegramBotClient telegramBot, long cid)
        {
            #region SearchAreaSession
            if (SearchArea.All(x => x.chatId != cid))
            {
                Console.WriteLine("For someone in ChatId " + cid + " new session has been created");
                SearchArea.Add(new _SearchArea(0, cid));
            }
            else
            {
                SearchArea.Single(x => x.chatId == cid).area = 0;
            }
            #endregion

            #region markup settings
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
            #endregion

            await telegramBot.SendTextMessageAsync(cid, "I'm ready to serve you 😉",
               ParseMode.Default, false, false, 0, Markup);
        }

        public static async void Command_SearchForMusic(ITelegramBotClient telegramBot, long cid)
        {
            #region SearchAreaSession
            if (SearchArea.All(x => x.chatId != cid))
            {
                Console.WriteLine("For someone in ChatId " + cid + " new session has been created");
                SearchArea.Add(new _SearchArea(1, cid));
            }
            else
            {
                SearchArea.Single(x => x.chatId == cid).area = 1;
            } 
            #endregion

            #region markup settings
            Markup.ResizeKeyboard = true;
            Markup.Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton(SearchForMusic),
                    new KeyboardButton(Back)
                }
            };
            #endregion

            await telegramBot.SendTextMessageAsync(cid, "Enter song name, sir! 🎵",
                ParseMode.Default, false, false, 0, Markup);
        }

        public static async void Command_SearchForAlbum(ITelegramBotClient telegramBot, long cid)
        {
            #region SearchAreaSession
            if (SearchArea.All(x => x.chatId != cid))
            {
                Console.WriteLine("For someone in ChatId " + cid + " new session has been created");
                SearchArea.Add(new _SearchArea(2, cid));
            }
            else
            {
                SearchArea.Single(x => x.chatId == cid).area = 2;
            } 
            #endregion

            #region markup settings
            Markup.ResizeKeyboard = true;
            Markup.Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton(SearchForAlbums), 
                    new KeyboardButton(Back)
                }
            };
            #endregion

            await telegramBot.SendTextMessageAsync(cid, "Enter album name, sir! 📓",
                ParseMode.Default, false, false, 0, Markup);
        }

        public static async void Command_SearchForArtist(ITelegramBotClient telegramBot, long cid)
        {
            #region SearchAreaSession
            if (SearchArea.All(x => x.chatId != cid))
            {
                Console.WriteLine("For someone in ChatId " + cid + " new session has been created");
                SearchArea.Add(new _SearchArea(3, cid));
            }
            else
            {
                SearchArea.Single(x => x.chatId == cid).area = 3;
            } 
            #endregion

            #region markup settings
            Markup.ResizeKeyboard = true;
            Markup.Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton(SearchForArtists), 
                    new KeyboardButton(Back)
                }
            };
            #endregion

            await telegramBot.SendTextMessageAsync(cid, "Enter artist name, sir! 💃🏼",
                ParseMode.Default, false, false, 0, Markup);
        }

        public static async void Command_GetRandomMusic(TelegramBotClient telegramBot, long cid)
        {
            #region SearchAreaSession
            if (SearchArea.All(x => x.chatId != cid))
            {
                Console.WriteLine("For someone in ChatId " + cid + " new session has been created");
                SearchArea.Add(new _SearchArea(4, cid));
            }
            else
            {
                SearchArea.Single(x => x.chatId == cid).area = 4;
            } 
            #endregion

            #region markup settings
            Markup.ResizeKeyboard = true;
            Markup.Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton(GetRandomMusic), 
                    new KeyboardButton(Back)
                }
            };
            #endregion

            await telegramBot.SendTextMessageAsync(cid, "Enter music genre to get awesome collection! 👑",
                ParseMode.Default, false, false, 0, Markup);
        }
    }
}