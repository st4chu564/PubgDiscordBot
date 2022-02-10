using Newtonsoft.Json;

namespace DiscordBot.Models.Pubg
{
    public class RosterRelationshipModel
    {
        [JsonProperty("team")]
        public BaseFields? Team { get; set; }

        [JsonProperty("participants")]
        public List<BaseFields>? Participants { get; set; }
    }
}
