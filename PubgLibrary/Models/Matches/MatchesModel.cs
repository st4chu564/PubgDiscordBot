using Newtonsoft.Json;
using PubgLibrary.Models;

namespace PubgLibrary.Models
{
    public class Matches
    {
        [JsonProperty("data")]
        public List<BaseFields>? MatchesList { get; set; }

        [JsonIgnore]
        public BaseFields? LatestMatch => MatchesList?.Where(entry => entry.Type == "match").FirstOrDefault();
    }
}
