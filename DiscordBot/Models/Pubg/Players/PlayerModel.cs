using Newtonsoft.Json;

namespace DiscordBot.Models.Pubg
{
    public class PlayerModel : BaseFields
    {

        [JsonProperty("attributes")]
        public PlayerAttributesModel? Attributes { get; set; }

        [JsonProperty("relationships")]
        public PlayerRelationshipsModel? Relationships { get; set; }

        [JsonProperty("links")]
        public LinksModel Links { get; set; }
    }

    public class PlayerShortModel
    {
        [JsonProperty("data")]
        public BaseFields? Data { get; set; }
    }

    public class PlayerAttributesModel
    {
        [JsonProperty("name")]
        public string? Name { get; set; } = string.Empty;

        [JsonProperty("stats")]
        public string? Stats { get; set; } = string.Empty;

        [JsonProperty("titleId")]
        public string? TitleId { get; set; } = string.Empty;

        [JsonProperty("shardId")]
        public string? ShardId { get; set; } = string.Empty;

        [JsonProperty("patchVersion")]
        public string? PatchVersion { get; set; } = string.Empty;
    }

    public class PlayerRelationshipsModel
    {
        [JsonProperty("assets")]
        public Assets? Assets { get; set; }

        [JsonProperty("matches")]
        public Matches? Matches { get; set; }
    }

    public class Assets
    {
        [JsonProperty("data")]
        public List<object>? AssetsList { get; set; }
    }

}
