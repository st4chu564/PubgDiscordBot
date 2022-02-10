using Newtonsoft.Json;

namespace DiscordBot.Models.Pubg
{
    public class MatchParticipantModel
    {
        [JsonProperty("stats")]
        public MatchParticipantStatsModel? Stats { get; set; }

        [JsonProperty("actor")]
        public string Actor { get; set; } = string.Empty;

        [JsonProperty("shardId")]
        public string Platform { get; set; } = string.Empty;
    }

    public class MatchParticipantStatsModel
    {

        [JsonProperty("DBNOs")]
        public int Dbnos { get; set; } = 0;

        [JsonProperty("assists")]
        public int Assists { get; set; } = 0;

        [JsonProperty("damageDealt")]
        public float DamageDealt { get; set; } = 0;

        [JsonProperty("deathType")]
        public string DeathType { get; set; } = string.Empty;

        [JsonProperty("headshotKills")]
        public int HeadshotKills { get; set; } = 0;

        [JsonProperty("heals")]
        public int Heals { get; set; } = 0;

        [JsonProperty("killPlace")]
        public int KillsPlace { get; set; } = 0;

        [JsonProperty("killsStreaks")]
        public int KillsStreaks { get; set; } = 0;

        [JsonProperty("kills")]
        public int Kills { get; set; } = 0;
        [JsonProperty("longestKills")]
        public float LongestKill { get; set; } = 0;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("playerId")]
        public string playerId { get; set; } = string.Empty;

        [JsonProperty("revives")]
        public int Revives { get; set; } = 0;

        [JsonProperty("rideDistance")]
        public float RideDistance { get; set; } = 0;

        [JsonProperty("roadKills")]
        public int RoadKills { get; set; } = 0;

        [JsonProperty("swimDistance")]
        public float SwimDistance { get; set; } = 0;

        [JsonProperty("teamKills")]
        public int TeamKills { get; set; } = 0;

        [JsonProperty("timeSurvived")]
        public int TimeSurvived { get; set; } = 0;

        [JsonProperty("vehiclesDestroyed")]
        public int VehiclesDestroyed { get; set; } = 0;

        [JsonProperty("walkDistance")]
        public float WalkDistance { get; set; } = 0;

        [JsonProperty("weaponsAcquired")]
        public int WeaponsAcquired { get; set; } = 0;

        [JsonProperty("winPlace")]
        public int WinPlace { get; set; } = 0;
    }
}
