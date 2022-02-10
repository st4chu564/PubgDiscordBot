using Newtonsoft.Json;
using System.Linq;

namespace DiscordBot.Models.Pubg
{
    public class Matches
    {
        [JsonProperty("data")]
        public List<BaseFields>? MatchesList { get; set; }

        [JsonIgnore]
        public BaseFields? LatestMatch => MatchesList?.Where(entry => entry.Type == "match").FirstOrDefault();
    }
}
