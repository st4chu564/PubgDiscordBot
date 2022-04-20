using Discord;
using Discord.Commands;
using DiscordBotShared.Helpers;
using PubgLibrary.Interfaces;
using PubgLibrary.Models;
using PubgLibrary.Services;

namespace PubgLibrary.Implementations
{


    [Group("pubg")]
    public class PubgCommandsModule : ModuleBase<CommandContext>, IPubgCommandsModule
    {
        private readonly IPubgApiService _pubgApi;

        public PubgCommandsModule(IPubgApiService pubgApi)
        {
            _pubgApi = pubgApi;
        }

        [Command("help")]
        [Summary("Returns a help message with all current messages")]
        public async Task Help()
        {
            EmbedBuilder builder = new EmbedBuilder()
            {
                Title = "Pomoc",
                Color = Color.Teal
            };

            var propListWithTranslation = ClassPropertyNameHelper<GameModeStatsModel>.GetPropertiesWithTranslation().Select(prop => $"{prop.Value}: {prop.Key.ToLower()}");
            var propListString = string.Join(", ", propListWithTranslation);

            builder.AddField(new EmbedFieldBuilder()
            {
                Name = "Stats: ",
                IsInline = true,
                Value = propListString.Substring(0, propListString.Length < 1024 ? propListString.Length : 1024)
            });

            builder.AddField(new EmbedFieldBuilder()
            {
                Name = "Player info",
                IsInline = false,
                Value = "{playerName}"
            });

            await ReplyAsync(string.Empty, false, builder.Build());
        }

        [Command("stats")]
        [Summary("Returns plyer Pubg stat based on theirs nickname")]
        public async Task GetBasicInfo(string statName)
        {
            string userName = GetUsername();

            if (userName != null)
            {
                var stats = await _pubgApi.GetPlayerStatsAsync(userName, statName);
                await ReplyAsync(string.Empty, false, stats);
                return;
            }
            await ReplyAsync("Nie znaleziono nazwy gracza");
            return;
        }

        [Command("lastMatch")]
        [Summary("Returns players latest match")]
        public async Task GetLatestMatch()
        {
            var username = GetUsername();

            var embed = await _pubgApi.GetPlayerLatestMatchAsync(username);

            await ReplyAsync(string.Empty, false, embed);

            return;
        }

        [Command("playerStats")]
        [Summary("Returns player Pubg stat based on theirs nickname")]
        public async Task GetPlayerInfo(string playerName = null)
        {
            var userName = playerName ?? GetUsername();

            if (userName != null)
            {
                var stats = await _pubgApi.GetPlayerStatsAsync(userName);
                await ReplyAsync("", false, stats);
                return;
            }
            await ReplyAsync("Nie znaleziono nazwy gracza");
            return;
        }

        [Command("seasonStats")]
        [Summary("Returns statistics for given season, if no season is give it gives stats for the latest season")]
        public async Task GetSeasonStats(string seasonNumber = null)
        {

        }

        private string GetUsername()
        {
            return (Context.User as Discord.WebSocket.SocketGuildUser).Nickname ?? Context.User.Username;
        }
    }
}
