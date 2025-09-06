using Newtonsoft.Json;

namespace NPMGUI.Core.Models
{
    internal class Package
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("version")]
        public string? Version { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("main")]
        public string? Main { get; set; }

        [JsonProperty("scripts")]
        public Dictionary<string, string>? Scripts { get; set; }

        [JsonProperty("repository")]
        public RepositoryInfo? Repository { get; set; }

        [JsonProperty("author")]
        public string? Author { get; set; }

        [JsonProperty("license")]
        public string? License { get; set; }

        [JsonProperty("dependencies")]
        public Dictionary<string, string>? Dependencies { get; set; }

        [JsonProperty("devDependencies")]
        public Dictionary<string, string>? DevDependencies { get; set; }
    }

    internal class RepositoryInfo
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }
}
