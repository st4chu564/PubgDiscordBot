using Newtonsoft.Json;
using PubgLibrary.Models;

namespace PubgLibrary.Models
{
    public class MatchRelationshipModel
    {
        [JsonProperty("rosters")]
        public BaseFieldsList? Rosters { get; set; }

        [JsonProperty("assets")]
        public BaseFieldsList? Assets { get; set; }
    }
}