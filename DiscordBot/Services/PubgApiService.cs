using Discord;
using DiscordBot.Models.Pubg;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Linq;
using System.Reflection;
using DiscordBot.Helpers;
using System.Text.RegularExpressions;
using System.Globalization;

namespace DiscordBot.Services
{
    public class PubgApiService : IPubgApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        #region URLs

        const string PubgApiUrl = "https://api.pubg.com/shards";
        const string PubgPlayerNameUrl = "players?filter[playerNames]";
        const string PubgPlayerIdUrl = "players";
        const string PubgPlayerLifetimeStatsUrl = "seasons/lifetime";
        const string PubgMatchesUrl = "matches";
        const string PubgLookupUrl = "https://pubglookup.com/players";
        const string ChickenDinnerUrl = "https://chickendinner.gg";

        #endregion

        #region Other consts

        const string PubgDefaultPlatform = "steam";
        const string PubgAccountIdFormat = @"^account\.[a-z0-9]+$";

        #endregion


        public PubgApiService(IConfiguration config = default)
        {
            _config = config;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.GetValue<string>("PubgApiKey"));
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.api+json");
        }

        public async Task<Embed> GetPlayerStats(string playerId, string statName = "kills", bool isPlayerId = false, CancellationToken cancelToken = default)
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

            var playerStatsRequest = await _httpClient.GetAsync($"{PubgApiUrl}/{PubgDefaultPlatform}/{PubgPlayerIdUrl}/{playerApiId}/{PubgPlayerLifetimeStatsUrl}");

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

        public async Task<Embed> GetPlayerLatestMatch(string playerId, bool isPlayerId = false, CancellationToken cancelToken = default)
        {
            PlayerResponseModel playerData = await GetPlayerData(playerId, cancelToken);
            MatchResponseModel latestMatch = await GetMatchData(playerData.Player.Relationships.Matches.LatestMatch.Id);

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
                field.Name = "Mapa: ";
                field.Value = latestMatch.Match.Attributes.MapName;
                field.IsInline = false;
            });

            builder.AddField(field =>
            {
                field.Name = "Link: ";
                field.Value = $"[Pubg lookup]({PubgLookupUrl}/{PubgDefaultPlatform}/{playerData.Player.Attributes.Name}/matches/{latestMatch.Match.Id}), [Chicken Dinner]({GetChickenDinnerMatchAddress(latestMatch.Assets.FirstOrDefault().GetAssetUrl(), playerData.Player.Attributes.Name)})";
                field.IsInline = false;
            });

            return builder.Build();
        }

        private async Task<MatchResponseModel?> GetMatchData(string matchId, CancellationToken cancelToken = default)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{PubgApiUrl}/{PubgDefaultPlatform}/{PubgMatchesUrl}/{matchId}");

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
            var request = await _httpClient.GetAsync($"{PubgApiUrl}/{PubgDefaultPlatform}/{PubgPlayerNameUrl}={playerName}", cancelToken);

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

            var requestResponse = await _httpClient.GetAsync($"{PubgApiUrl}/{PubgDefaultPlatform}/{PubgPlayerIdUrl}/{playerApiId}");

            var test = await requestResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PlayerResponseModel>(test);
        }

        private bool IsPlayerIdFormat(string playerId)
        {
            return Regex.IsMatch(playerId, PubgAccountIdFormat);
        }

        private string GetChickenDinnerMatchAddress(string telemetryFileName, string playerToFollow)
        {
            return $"{ChickenDinnerUrl}/{PubgDefaultPlatform}/{telemetryFileName}?follow={playerToFollow}";
        }
    }
}
