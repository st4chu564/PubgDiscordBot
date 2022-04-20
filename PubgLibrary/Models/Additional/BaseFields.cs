using Newtonsoft.Json;

namespace PubgLibrary.Models
{
    public class BaseFields
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }

        public bool IsIdGuid()
        {
            return Guid.TryParse(Id, out _);
        }
    }

    public class BaseFieldsList
    {
        [JsonProperty("data")]
        public List<BaseFields> Data { get; set; }
    }
}
