using Newtonsoft.Json;

namespace DiscordBot.Models.Pubg
{
    public class PlayerLifeTimeStatsResponseModel
    {
        [JsonProperty("data")]
        public PlayerLifeTimeStatsData Data { get; set; }

        [JsonProperty("links")]
        public Dictionary<string, string>? Links { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string>? Meta { get; set; }
    }

    public class PlayerLifeTimeStatsData
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("attributes")]
        public PlayerLifeTimeStatsAttributes? Attributes { get; set; }

        [JsonProperty("relationships")]
        public PlayerLifeTimeRelationships? Relationships { get; set; }
    }

    public class PlayerLifeTimeStatsAttributes
    {
        [JsonProperty("bestRankPoint")]
        public int BestRankPoint { get; set; }
        [JsonProperty("gameModeStats")]
        public Dictionary<string, GameModeStatsModel>? GameModeStats { get; set; }

        [JsonIgnore]
        public Dictionary<string, GameModeStatsModel>? GameModesFpp => GameModeStats?.Where(x => x.Key.ToLower().Contains("fpp"))
            .ToDictionary(x => x.Key, y => y.Value);

        [JsonIgnore]
        public Dictionary<string, GameModeStatsModel>? GameModesTpp => GameModeStats?.Where(x => !x.Key.ToLower().Contains("fpp"))
            .ToDictionary(x => x.Key, y => y.Value);
    }

    public class PlayerLifeTimeRelationships
    {
        [JsonProperty("matchesSolo")]
        public Matches? MatchesSolo { get; set; }

        [JsonProperty("matchesSoloFPP")]
        public Matches? MatchesSoloFPP { get; set; }

        [JsonProperty("matchesDuo")]
        public Matches? MatchesDuo { get; set; }

        [JsonProperty("matchesDuoFPP")]
        public Matches? MatchesDuoFPP { get; set; }

        [JsonProperty("matchesSquad")]
        public Matches? MatchesSquad { get; set; }

        [JsonProperty("matchesSquadFPP")]
        public Matches? MatchesSquadFPP { get; set; }

        [JsonProperty("season")]
        public SeasonData? Season { get; set; }

        [JsonProperty("player")]
        public PlayerShortModel? Player { get; set; }


    }

    public class SeasonData
    {
        [JsonProperty("data")]
        public BaseFields? Data { get; set; }
    }
}
