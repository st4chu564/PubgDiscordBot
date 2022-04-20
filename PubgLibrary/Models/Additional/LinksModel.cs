using Newtonsoft.Json;

namespace PubgLibrary.Models
{
    public class LinksModel
    {
        [JsonProperty("self")]
        public string Self { get; set; } = string.Empty;

        [JsonProperty("schema")]
        public string Schema { get; set; } = string.Empty;
    }
}
