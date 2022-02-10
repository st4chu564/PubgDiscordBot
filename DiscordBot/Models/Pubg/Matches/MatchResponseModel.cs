using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordBot.Models.Pubg
{
    public class MatchResponseModel
    {
        [JsonProperty("data")]
        public MatchModel? Match { get; set; }

        [JsonProperty("included")]
        public List<IncludedObject>? IncludedData { get; set; }

        [JsonProperty("links")]
        public LinksModel? Links { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string>? Meta { get; set; }

        [JsonIgnore]
        public IEnumerable<MatchParticipantModel> Participants => IncludedData
            .Where(data => data.Type == "participant")
            .Select(data => ((JObject)data.Attributes).ToObject<MatchParticipantModel>());

        [JsonIgnore]
        public IEnumerable<MatchRosterModel> Rosters => IncludedData
            .Where(data => data.Type == "roster")
            .Select(data => ((JObject)data.Attributes).ToObject<MatchRosterModel>());

        [JsonIgnore]
        public IEnumerable<MatchAssetModel> Assets => IncludedData
            .Where(data => data.Type == "asset")
            .Select(data => ((JObject)data.Attributes).ToObject<MatchAssetModel>());
    }
}
