using Newtonsoft.Json;

namespace DiscordBot.Models.Pubg
{
    public class MatchRelationshipModel
    {
        [JsonProperty("rosters")]
        public BaseFieldsList? Rosters { get; set; }

        [JsonProperty("assets")]
        public BaseFieldsList? Assets { get; set; }
    }
}