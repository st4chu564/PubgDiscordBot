using Newtonsoft.Json;

namespace PubgLibrary.Models
{
    public class MatchRosterModel
    {
        [JsonProperty("stats")]
        public MatchRosterStatsModel? MatchRosterStats { get; set; }

        [JsonProperty("won")]
        public bool Won { get; set; } = false;

        [JsonProperty("shardId")]
        public string Platform { get; set; } = string.Empty;
    }

    public class MatchRosterStatsModel
    {
        [JsonProperty("rank")]
        public int Rank { get; set; } = 0;

        [JsonProperty("teamId")]
        public int TeamId { get; set; } = 0;
    }
}
