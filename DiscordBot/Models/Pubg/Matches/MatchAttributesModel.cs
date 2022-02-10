using Newtonsoft.Json;

namespace DiscordBot.Models.Pubg
{
    public class MatchAttributesModel
    {
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;

        [JsonProperty("titleId")]
        public string TitleId { get; set; } = string.Empty;

        [JsonProperty("shardId")]
        public string Platform { get; set; } = string.Empty;

        [JsonProperty("tags")]
        public string Tags { get; set; } = string.Empty;

        [JsonProperty("mapName")]
        public string MapName { get; set; } = string.Empty;

        [JsonProperty("matchType")]
        public string MatchType { get; set; } = string.Empty;

        [JsonProperty("duration")]
        public int Duration { get; set; } = -1;

        [JsonProperty("stats")]
        public string Stats { get; set; } = string.Empty;

        [JsonProperty("gameMode")]
        public string GameMode { get; set; } = string.Empty;

        [JsonIgnore]
        public GameModesEnum GameModeEnum => GameModesEnumExtensions.GetGameModeEnumValueByString(GameMode);

        [JsonProperty("isCustomMatch")]
        public bool IsCustomMatch { get; set; } = false;

        [JsonProperty("seasonState")]
        public string SeasonState { get; set; } = string.Empty;
    }
}
