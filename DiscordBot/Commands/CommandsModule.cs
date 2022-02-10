using Discord;
using Discord.Commands;
using DiscordBot.Helpers;
using DiscordBot.Models.Pubg;
using DiscordBot.Services;
using System.Linq;

namespace DiscordBot.Commands
{
    [Group("basic")]
    public class CommandsModule : ModuleBase<CommandContext>
    {

        [Command("say")]
        [Summary("Returns what you entered")]
        [Alias("Powtorz")]
        public async Task RepeatAsync([Remainder] [Summary("Enter text to repeat")]
        string text) => await ReplyAsync(text);
    }

    [Group("pubg")]
    public class PubgCommandsModule : ModuleBase<CommandContext>
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

            try
            {
                builder.AddField(new EmbedFieldBuilder()
                {
                    Name = "Stats: (wszystkie dostępne",
                    IsInline = true,
                    Value = string.Join(", ", propListWithTranslation).Substring(0, 1024)
                });
            }
            catch(Exception ex)
            {
                string test = "ex";
            }

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

            if(userName != null)
            {
                var stats = await _pubgApi.GetPlayerStats(userName, statName);
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

            var embed = await _pubgApi.GetPlayerLatestMatch(username);

            await ReplyAsync(string.Empty, false, embed);

            return;
        }

        [Command("playerStats")]
        [Summary("Returns player Pubg stat based on theirs nickname")]
        public async Task GetPlayerInfo(string playerName)
        {
            var userName = playerName ?? Context.User.Username;

            if (userName != null)
            {
                var stats = await _pubgApi.GetPlayerStats(userName);
                await ReplyAsync("", false, stats);
                return;
            }
            await ReplyAsync("Nie znaleziono nazwy gracza");
            return;
        }

        private string GetUsername()
        {
            return (Context.User as Discord.WebSocket.SocketGuildUser).Nickname ?? Context.User.Username;
        }
    }
}
