using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace DiscordBot.Models.Pubg
{
    public class MatchAssetModel
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;

        [JsonProperty("URL")]
        public string Url { get; set; } = string.Empty;

        private const string AssetUrlRegex = @"(\d{4}(\/\d{2}){4}\/([a-z0-9]{8}(\-[a-z0-9]{4}){3}\-[a-z0-9]{12}))";

        public string GetAssetUrl()
        {
            var s = Regex.Match(Url, AssetUrlRegex);

            return s.Groups[0].Value;
        }
    }
}
