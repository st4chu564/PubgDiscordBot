using Newtonsoft.Json;

namespace DiscordBot.Models.Pubg
{
    public class MatchModel : BaseFields
    {
        [JsonProperty("attributes")]
        public MatchAttributesModel? Attributes { get; set; }

        [JsonProperty("relationships")]
        public MatchRelationshipModel? Relationships { get; set; }

        [JsonProperty("links")]
        public LinksModel? Links { get; set; }
    }
}