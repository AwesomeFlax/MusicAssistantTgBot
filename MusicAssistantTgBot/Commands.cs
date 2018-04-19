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
    internal static class Commands
    {
        private static ReplyKeyboardMarkup Markup = new ReplyKeyboardMarkup();
        private static InlineKeyboardMarkup Inline = new InlineKeyboardMarkup(GetInlineKeyboard(new[] { Previous, Next }));
        private static int _searchArea;
        private const string BaseLink = "http://musicrestwebapi.azurewebsites.net/";

        #region Commands strings
        private const string Start = "/start";
        private const string Back = "Go back to main menu";
        private const string Help = "/help";
        private const string SearchForMusic = "Search for music";
        private const string SearchForAlbums = "Search for albums";
        private const string SearchForArtists = "Search for artists";
        private const string GetRandomMusic = "Get some good music";
        private const string Next = "➡️";
        private const string Previous = "⬅️";
        #endregion

        private static readonly string[] commands =
        {
            Start, Back, Help, SearchForAlbums, SearchForArtists, SearchForMusic, GetRandomMusic
        };
        private static readonly List<string> CommandList = new List<string>(commands);

        public static bool CheckIfMessageIsCommand(string command)
        {
            return CommandList.Contains(command);
        }

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
                    Command_Start(telegramBot, cid);
                    break;

                case Back:
                    Command_Back(telegramBot, cid);
                    break;

                case SearchForMusic:
                    Command_SearchForMusic(telegramBot, cid);
                    break;

                case SearchForAlbums:
                    Command_SearchForAlbum(telegramBot, cid);
                    break;

                case SearchForArtists:
                    Command_SearchForArtist(telegramBot, cid);
                    break;
            }
        }

        public static void GetParametres(string extension, TelegramBotClient telegramBot, long cid, int mid)
        {
            switch (_searchArea)
            {
                case 1:
                    Response_Song(telegramBot, cid, mid, extension);
                    break;

                case 2:
                    Response_Album(telegramBot, cid, extension);
                    break;

                case 3:
                    Response_Artist(telegramBot, cid, extension);
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
                    Inline_Next(command, telegramBot, cid, mid);
                    break;

                case Previous:
                    Inline_Previous(command, telegramBot, cid, mid);
                    break;
            }
        }


        private static async void Command_Start(ITelegramBotClient telegramBot, long cid)
        {
            _searchArea = 0;

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

        private static async void Command_Back(ITelegramBotClient telegramBot, long cid)
        {
            _searchArea = 0;

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


        private static async void Command_SearchForMusic(ITelegramBotClient telegramBot, long cid)
        {
            _searchArea = 1;

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

        private static async void Command_SearchForAlbum(ITelegramBotClient telegramBot, long cid)
        {
            _searchArea = 2;

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

        private static async void Command_SearchForArtist(ITelegramBotClient telegramBot, long cid)
        {
            _searchArea = 3;

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


        private static async void Response_Song(ITelegramBotClient telegramBot, long cid, int mid, string extension)
        {
            try
            {
                await telegramBot.SendTextMessageAsync
                        (cid, "Wait a bit, we're doing some magic ✨🔮");

                var songs = JsonConvert.DeserializeObject<List<Song>>
                    (new WebClient().DownloadString(BaseLink + "songs"));

                var fittingSongs = songs
                    .Where(s => s.name.ToLower().Contains(extension.ToLower()))
                    .ToList();

                var botResponse = new List<string>();

                foreach (var fittingSong in fittingSongs)
                {
                    var youTubeLink = new WebClient().DownloadString(BaseLink + "songs/" + fittingSong.id + "/youtube").Trim('"');

                    var album = JsonConvert.DeserializeObject<Album>
                        (new WebClient().DownloadString(BaseLink + "albums/" + fittingSong.album.id));

                    var artist = JsonConvert.DeserializeObject<Artist>
                        (new WebClient().DownloadString(BaseLink + "artists/" + album.artist.id));

                    botResponse.Add($"<b>[1/{fittingSongs.Count}]</b>" +
                                    $"{artist.nickName} - {fittingSong.name}\n" +
                                    $"({youTubeLink})\n");
                }

                if (botResponse.Count > 0)
                {
                    await telegramBot.SendTextMessageAsync(cid, botResponse[0],
                        ParseMode.Html, false, false, 0, Inline);
                }
                else
                {
                    await telegramBot.SendTextMessageAsync
                        (cid, "Sorry, we didn't find at least one song with such name in our database 😱");
                }

                _searchArea = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ({0}) - {1}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), ex.Message);

                await telegramBot.SendTextMessageAsync
                        (cid, "Something went wrong, please, try again later 😔");
            }
        }

        private static async void Response_Album(ITelegramBotClient telegramBot, long cid, string extension)
        {
            try
            {
                await telegramBot.SendTextMessageAsync
                    (cid, "Wait a bit, we're doing some magic ✨🔮");

                var albums = JsonConvert.DeserializeObject<List<Album>>
                    (new WebClient().DownloadString(BaseLink + "albums"));

                var fittingAlbums = albums
                    .Where(a => a.name.ToLower().Contains(extension.ToLower()))
                    .ToList();

                var botResponse = new List<string>();

                foreach (var fittingAlbum in fittingAlbums)
                {
                    botResponse.Add($"Album Name: {fittingAlbum.name}\n" +
                                    $"Genre: {fittingAlbum.genre}\n" +
                                    $"Date of Release: {fittingAlbum.releaseDate}\n" +
                                    $"Description: {fittingAlbum.description}\n" +
                                    $"Artist Name: {fittingAlbum.artist.nickName}\n" +
                                    $"{fittingAlbum.albumCoverUrl}\n");
                }

                if (botResponse.Count > 0)
                {
                    foreach (var message in botResponse)
                    {
                        await telegramBot.SendTextMessageAsync(cid, message);
                    }
                }
                else
                {
                    await telegramBot.SendTextMessageAsync
                        (cid, "Sorry, we didn't find at least one album with such name in our database 😱");
                }

                _searchArea = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: ({0}) - {1}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), e.Message);

                await telegramBot.SendTextMessageAsync
                    (cid, "Something went wrong, please, try again later 😔");
            }
        }

        private static async void Response_Artist(ITelegramBotClient telegramBot, long cid, string extension)
        {
            try
            {
                await telegramBot.SendTextMessageAsync
                    (cid, "Wait a bit, we're doing some magic ✨🔮");

                var artists = JsonConvert.DeserializeObject<List<Artist>>
                    (new WebClient().DownloadString(BaseLink + "artists"));

                var fittingArtists = artists
                    .Where(a => a.nickName.ToLower().Contains(extension.ToLower()))
                    .ToList();

                var botResponse = new List<string>();

                foreach (var fittingArtist in fittingArtists)
                {
                    botResponse.Add($"Nickname: {fittingArtist.nickName}\n" +
                                    $"First Name: {fittingArtist.firstName}\n" +
                                    $"Last Name: {fittingArtist.lastName}\n" +
                                    $"Birth Date: {fittingArtist.birthDate}\n" +
                                    $"Birth Place: {fittingArtist.birthPlace}\n" +
                                    $"Years Active: {fittingArtist.careerStart} - {fittingArtist.careerEnd}\n" +
                                    $"{fittingArtist.artistPhotoUrl}\n");
                }

                if (botResponse.Count > 0)
                {
                    foreach (var message in botResponse)
                    {
                        await telegramBot.SendTextMessageAsync(cid, message);
                    }
                }
                else
                {
                    await telegramBot.SendTextMessageAsync
                        (cid, "Sorry, we didn't find at least one artist with such name in our database 😱");
                }

                _searchArea = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: ({0}) - {1}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), e.Message);

                await telegramBot.SendTextMessageAsync
                    (cid, "Something went wrong, please, try again later 😔");
            }
        }


        private static async void Inline_Next(string command, ITelegramBotClient telegramBot, long cid, int mid)
        {
            await telegramBot.EditMessageTextAsync(cid, mid, "updated next", ParseMode.Default, false, Inline);
        }

        private static async void Inline_Previous(string command, ITelegramBotClient telegramBot, long cid, int mid)
        {
            await telegramBot.EditMessageTextAsync(cid, mid, "updated prev", ParseMode.Default, false, Inline);
        }
    }
}
