using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MusicAssistantTgBot.Models;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MusicAssistantTgBot
{
    public class Response : Management
    {  
        public static async void Response_Song(ITelegramBotClient telegramBot, long cid, int mid, string extension)
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

                SearchArea = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ({0}) - {1}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), ex.Message);

                await telegramBot.SendTextMessageAsync
                        (cid, "Something went wrong, please, try again later 😔");
            }
        }

        public static async void Response_Album(ITelegramBotClient telegramBot, long cid, string extension)
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

                SearchArea = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: ({0}) - {1}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), e.Message);

                await telegramBot.SendTextMessageAsync
                    (cid, "Something went wrong, please, try again later 😔");
            }
        }

        public static async void Response_Artist(ITelegramBotClient telegramBot, long cid, string extension)
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

                SearchArea = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: ({0}) - {1}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), e.Message);

                await telegramBot.SendTextMessageAsync
                    (cid, "Something went wrong, please, try again later 😔");
            }
        }
    }
}