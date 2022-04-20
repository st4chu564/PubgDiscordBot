using Newtonsoft.Json;
using PubgLibrary.Models;

namespace PubgLibrary.Models
{
    public class IncludedObject : BaseFields
    {
        [JsonProperty("attributes")]
        public dynamic Attributes { get; set; }

        [JsonProperty("relationships")]
        public MatchRelationshipModel? Relationships { get; set; }

    }
}
