using Discord;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using DiscordBotShared.Helpers;
using PubgLibrary.Implementations;
using PubgLibrary.Models;
using System.Net.Http.Headers;
using System.Reflection;
using DiscordBotShared.Interfaces;
using System.Text.RegularExpressions;

namespace PubgLibrary.Services
{
    public class PubgApiService : IPubgApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly Urls _urls = new Urls();

        #region Other consts

        const string PubgDefaultPlatform = "steam";
        const string PubgAccountIdFormat = @"^account\.[a-z0-9]+$";

        #endregion

        /// <summary>
        /// Constructor for Pubg Api Service
        /// </summary>
        /// <param name="config"></param>
        public PubgApiService(IConfiguration config, IUrls urls)
        {
            _config = config;
            _urls = (Urls)urls;
            _urls.Initialize(config);
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["PubgApiKey"]);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.api+json");
        }

        /// <summary>
        /// A method for getting and building Embed message for given player stats
        /// </summary>
        /// <param name="playerId">Requested player name or Id, if <paramref name="isPlayerId"/> equals is false, player id will be acquired from API</param>
        /// <param name="statName">Selected stat name </param>
        /// <param name="isPlayerId"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public async Task<Embed> GetPlayerStatsAsync(string playerId, string statName = "kills", bool isPlayerId = false, CancellationToken cancelToken = default)
        {
            string playerApiId = playerId;
            if (!isPlayerId)
            {
                playerApiId = await GetPlayerId(playerId, cancelToken);

                if (playerId == null)
                {
                    return null;
                }
            }

            var playerStatsRequest = await _httpClient.GetAsync($"{_urls.PubgApiUrl}/{PubgDefaultPlatform}/{_urls.PubgPlayerIdUrl}/{playerApiId}/{_urls.PubgPlayerLifetimeStatsUrl}");

            var statsModel = JsonConvert.DeserializeObject<PlayerLifeTimeStatsResponseModel>(await playerStatsRequest.Content.ReadAsStringAsync());

            Type gmType = typeof(GameModeStatsModel);

            var properties = gmType.GetProperties();

            EmbedBuilder embedBuilder = new EmbedBuilder()
            {
                Title = $"{properties.FirstOrDefault(field => field.Name.ToLower() == statName).GetCustomAttribute<TranslationAttribute>().Translation}",
                Color = Color.Blue
            };

            if (gmType.GetProperties().FirstOrDefault(prop => prop.Name.ToLower() == statName) == null)
            {
                return null;
            }

            foreach (var gameMode in statsModel.Data.Attributes.GameModesFpp)
            {
                embedBuilder.AddField(field =>
                {
                    field.Name = $"{gameMode.Key}";
                    field.Value = properties.FirstOrDefault(field => field.Name.ToLower() == statName).GetValue(gameMode.Value);
                    field.IsInline = true;
                });
            }

            foreach (var gameMode in statsModel.Data.Attributes.GameModesTpp)
            {
                embedBuilder.AddField(field =>
                {
                    field.Name = $"{gameMode.Key}";
                    field.Value = properties.FirstOrDefault(field => field.Name.ToLower() == statName).GetValue(gameMode.Value);
                    field.IsInline = true;
                });
            }

            return embedBuilder.Build();

        }

        public async Task<Embed> GetPlayerLatestMatchAsync(string playerId, bool isPlayerId = false, CancellationToken cancelToken = default)
        {
            PlayerResponseModel playerData = await GetPlayerData(playerId, cancelToken);
            MatchResponseModel latestMatch = await GetMatchData(playerData.Player.Relationships.Matches.LatestMatch.Id);

            var participantData = latestMatch.GetParticipantData(playerData.Player.Id);

            EmbedBuilder builder = new EmbedBuilder()
            {
                Title = "Ostatni mecz",
                Color = Color.Green
            };

            builder.AddField(field =>
            {
                field.Name = "Gracz: ";
                field.Value = playerData.Player.Attributes.Name;
                field.IsInline = true;
            });

            builder.AddField(field =>
            {
                field.Name = "Data rozpoczęcia: ";
                field.Value = latestMatch.Match.Attributes.CreatedAt.ToString("g");
                field.IsInline = true;
            });

            builder.AddField(field =>
            {
                field.Name = "Czas trwania: ";
                field.Value = TimeSpan.FromSeconds(latestMatch.Match.Attributes.Duration);
                field.IsInline = true;
            });

            builder.AddField(field =>
            {
                field.Name = "Miejsce: ";
                field.Value = participantData.Stats.WinPlace;
                field.IsInline = true;
            });

            builder.AddField(field =>
            {
                field.Name = "Zabójstwa: ";
                field.Value = participantData.Stats.Kills;
                field.IsInline = false;
            });

            builder.AddField(field =>
            {
                field.Name = "Obrażenia: ";
                field.Value = participantData.Stats.DamageDealt;
                field.IsInline = true;
            });

            builder.AddField(field =>
            {
                field.Name = "Mapa: ";
                field.Value = latestMatch.Match.Attributes.MapName;
                field.IsInline = false;
            });

            builder.AddField(field =>
            {
                field.Name = "Link: ";
                field.Value = $"[Pubg lookup]({GetPubgLookupFormat(playerData.Player.Attributes.Name, latestMatch.Match.Id, _urls.PubgLookupUrl)}), [Chicken Dinner]({GetChickenDinnerMatchAddress(latestMatch.Assets.FirstOrDefault().GetAssetUrl(), playerData.Player.Attributes.Name, _urls.ChickenDinnerUrl)})";
                field.IsInline = false;
            });

            builder.Footer = new EmbedFooterBuilder();

            builder.Footer.Text = "Made by St4chu";

            builder.Footer.Build();

            return builder.Build();
        }

        public async Task<SeasonData> GetSeasonDataAsync(string seasonNumber)
        {
            return null;
        }

        private async Task<MatchResponseModel?> GetMatchData(string matchId, CancellationToken cancelToken = default)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_urls.PubgApiUrl}/{PubgDefaultPlatform}/{_urls.PubgMatchesUrl}/{matchId}");

                var text = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<MatchResponseModel>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<string?> GetPlayerId(string playerName, CancellationToken cancelToken = default)
        {
            //TODO Add some kind of caching to this method to prevent from sending request everytime
            var request = await _httpClient.GetAsync($"{_urls.PubgApiUrl}/{PubgDefaultPlatform}/{_urls.PubgPlayerNameUrl}={playerName}", cancelToken);

            if (request.IsSuccessStatusCode)
            {
                try
                {
                    var model = JsonConvert.DeserializeObject<PlayersResponseModel>(await request.Content.ReadAsStringAsync());
                    return model?.Player?[0]?.Id;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return null;
        }

        private async Task<PlayerResponseModel?> GetPlayerData(string playerId, CancellationToken cancelToken = default)
        {
            string playerApiId = playerId;
            if (!IsPlayerIdFormat(playerId))
            {
                playerApiId = await GetPlayerId(playerId, cancelToken);

                if (playerId == null)
                {
                    return null;
                }
            }

            var requestResponse = await _httpClient.GetAsync($"{_urls.PubgApiUrl}/{PubgDefaultPlatform}/{_urls.PubgPlayerIdUrl}/{playerApiId}");

            return JsonConvert.DeserializeObject<PlayerResponseModel>(await requestResponse.Content.ReadAsStringAsync());
        }

        private bool IsPlayerIdFormat(string playerId)
        {
            return Regex.IsMatch(playerId, PubgAccountIdFormat);
        }

        private static string GetChickenDinnerMatchAddress(string telemetryFileName, string playerToFollow, string chickenDinnerUrl) => $"{chickenDinnerUrl}/{PubgDefaultPlatform}/{telemetryFileName}?follow={playerToFollow}";

        private static string GetPubgLookupFormat(string playerName, string matchId, string pubgLookupUrl) => $"{pubgLookupUrl}/{PubgDefaultPlatform}/{playerName}/matches/{matchId}";
    }
}
