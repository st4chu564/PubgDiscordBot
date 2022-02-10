using Newtonsoft.Json;

namespace DiscordBot.Models.Pubg
{
    public class PlayerResponseModel
    {
        [JsonProperty("data")]
        public PlayerModel? Player { get; set; }

        [JsonProperty("links")]
        public Dictionary<string, string>? Links { get; set; }

        [JsonProperty("meta")]
        public object? Meta { get; set; }
    }

    public class PlayersResponseModel
    {
        [JsonProperty("data")]
        public PlayerModel[]? Player { get; set; }

        [JsonProperty("links")]
        public Dictionary<string, string>? Links { get; set; }

        [JsonProperty("meta")]
        public object? Meta { get; set; }

    }
}
