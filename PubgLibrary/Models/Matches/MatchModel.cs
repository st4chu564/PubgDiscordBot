using Newtonsoft.Json;
using PubgLibrary.Models;

namespace PubgLibrary.Models
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