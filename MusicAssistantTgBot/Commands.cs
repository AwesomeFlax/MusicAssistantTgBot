using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MusicAssistantTgBot
{
    static class Commands
    {
        static ReplyKeyboardMarkup markup = new ReplyKeyboardMarkup();
        static int searchArea = 0;
        const string baseLink = "http://musicrestwebapi.azurewebsites.net/";

        #region Commands strings
        const string Start = "/start";
        const string Back = "Go back to main menu";
        const string Help = "/help";
        const string SearchFormMusic = "Search for music";
        const string SearchForAlbums = "Search for albums";
        const string SearchFormArtists = "Search for artists";
        const string GetRandomMusic = "Get some good music";
        #endregion

        static string[] commands = { Start, Back, Help, SearchForAlbums, SearchFormArtists, SearchFormMusic, GetRandomMusic };
        static List<string> commandList = new List<string>(commands);

        public static bool CheckIfMessageIsCommand(string command)
        {
            if (commandList.Contains(command))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static void GetCommand
            (string command, TelegramBotClient telegramBot, long cid)
        {
            switch (command)
            {
                case Start:
                    Command_Start(telegramBot, cid); break;

                case Back:
                    Command_Back(telegramBot, cid); break;

                case SearchFormMusic:
                    Command_SearchFormMusic(telegramBot, cid); break;

                default:
                    break;
            }
        }

        public static void GetParametres
            (string extension, TelegramBotClient telegramBot, long cid)
        {
            switch (searchArea)
            {
                case 1:
                    Response_Song(telegramBot, cid, extension);
                    break;

                default:
                    telegramBot.SendTextMessageAsync
                        (cid, "Beeing honest - have no idea what are you talking about 🤔");
                    break;
            }
        }


        private static async void Command_Start
            (TelegramBotClient telegramBot, long cid)
        {
            searchArea = 0;

            markup.ResizeKeyboard = true;
            markup.Keyboard = new KeyboardButton[][]
            {
                                new KeyboardButton[]
                                {
                                   new KeyboardButton(SearchFormMusic),
                                    new KeyboardButton(SearchForAlbums)
                                },
                                new KeyboardButton[]
                                {
                                    new KeyboardButton(SearchForAlbums),
                                    new KeyboardButton(GetRandomMusic)
                                }
            };

            await telegramBot.SendTextMessageAsync(cid, "Ok, lets do this 😈",
                ParseMode.Default, false, false, 0, markup);
        }

        private static async void Command_Back
            (TelegramBotClient telegramBot, long cid)
        {
            searchArea = 0;

            markup.ResizeKeyboard = true;
            markup.Keyboard = new KeyboardButton[][]
            {
                                new KeyboardButton[]
                                {
                                    new KeyboardButton(SearchFormMusic),
                                    new KeyboardButton(SearchForAlbums)
                                },
                                new KeyboardButton[]
                                {
                                    new KeyboardButton(SearchForAlbums),
                                    new KeyboardButton(GetRandomMusic)
                                }
            };

            await telegramBot.SendTextMessageAsync(cid, "I'm ready to serve you 😉",
               ParseMode.Default, false, false, 0, markup);
        }

        private static async void Command_SearchFormMusic
            (TelegramBotClient telegramBot, long cid)
        {
            searchArea = 1;

            #region markup settings
            markup.ResizeKeyboard = true;
            markup.Keyboard = new KeyboardButton[][]
            {
                                new KeyboardButton[]
                                {
                                    new KeyboardButton(Back)
                                }
            };
            #endregion

            await telegramBot.SendTextMessageAsync(cid, "Enter song name, sir! 🎵",
                ParseMode.Default, false, false, 0, markup);
        }


        private static async void Response_Song
            (TelegramBotClient telegramBot, long cid, string extension)
        {
            try
            {
                await telegramBot.SendTextMessageAsync
                        (cid, "Wait a bit, we're doing some magic ✨🔮");

                var songs = JsonConvert.DeserializeObject<List<Song>>
                    (new WebClient().DownloadString(baseLink + "songs"));

                var fitSongs = songs
                    .Where(s => s.name.ToLower().Contains(extension.ToLower()))
                    .ToList();

                StringBuilder botResponse = new StringBuilder();
                foreach (var fitSong in fitSongs)
                {
                    string youTubeLink = new WebClient().DownloadString(baseLink + "songs/" + fitSong.id + "/youtube").Trim('"');

                    var album = JsonConvert.DeserializeObject<Album>
                        (new WebClient().DownloadString(baseLink + "albums/" + fitSong.album.id));

                    var artist = JsonConvert.DeserializeObject<Artist>
                        (new WebClient().DownloadString(baseLink + "artists/" + album.artist.id));

                    botResponse.Append(artist.nickName + " — " + fitSong.name);
                    botResponse.Append(" (" + youTubeLink + ")" + "\n");
                }

                if (botResponse.Length > 0)
                {
                    await telegramBot.SendTextMessageAsync
                        (cid, botResponse.ToString());
                }
                else
                {
                    await telegramBot.SendTextMessageAsync
                        (cid, "Sorry, we didn't find at least one song with such name in our database 😱");
                }

                searchArea = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ({0}) - {1}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), ex.Message);

                await telegramBot.SendTextMessageAsync
                        (cid, "Something went wrong, please, try again later 😔");
            }
        }
    }
}
