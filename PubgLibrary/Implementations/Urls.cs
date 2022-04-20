using DiscordBotShared.Interfaces;
using Microsoft.Extensions.Configuration;

namespace PubgLibrary.Implementations
{
    public class Urls : IUrls
    {
        public string PubgApiUrl { get; private set; } = string.Empty;
        public string PubgPlayerNameUrl { get; private set; } = string.Empty;
        public string PubgPlayerIdUrl { get; private set; } = string.Empty;
        public string PubgPlayerLifetimeStatsUrl { get; private set; } = string.Empty;
        public string PubgMatchesUrl { get; private set; } = string.Empty;
        public string PubgLookupUrl { get; private set; } = string.Empty;
        public string ChickenDinnerUrl { get; private set; } = string.Empty;

        public void Initialize(IConfiguration config)
        {
            PubgApiUrl = config["PubgApiUrl"] ?? "https://api.pubg.com/shards";
            PubgPlayerNameUrl = config["PubgPlayerNameUrl"] ?? "players?filter[playerNames]";
            PubgPlayerIdUrl = config["PubgPlayerIdUrl"] ?? "players";
            PubgPlayerLifetimeStatsUrl = config["PubgPlayerLifetimeStatsUrl"] ?? "seasons/lifetime";
            PubgMatchesUrl = config["PubgMatchesUrl"] ?? "matches";
            PubgLookupUrl = config["PubgLookupUrl"] ?? "https://pubglookup.com/players";
            ChickenDinnerUrl = config["PubgChickenDinnerUrlApiKey"] ?? "https://chickendinner.gg";
        }
    }
}
