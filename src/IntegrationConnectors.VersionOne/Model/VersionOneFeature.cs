using Newtonsoft.Json;

namespace AutomationConnectors.VersionOne.Model
{
    public class VersionOneFeature
    {
        public string Number { get; set; }
        [JsonProperty("Priority.Name")]
        public string Priority { get; set; }
        public string Name { get; set; }
        [JsonProperty("Status.Name")]
        public string Status { get; set; }
        public string Team { get; set; }
        [JsonProperty("Scope.Name")]
        public string Scope { get; set; }
        public VersionOneAssetState State { get; set; }
        public long Order { get; set; }
        [JsonProperty("_oid")]
        public string Url { get; set; }
    }
}
