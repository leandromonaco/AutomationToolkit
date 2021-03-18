using Newtonsoft.Json;
using System;

namespace AutomationConnectors.VersionOne.Model
{
    public class VersionOneIssue
    {
        public string Number { get; set; }
        public string Name { get; set; }
        [JsonProperty("Team.Name")]
        public string Team { get; set; }
        [JsonProperty("Category.Name")]
        public string Category { get; set; }
        public string IdentifiedBy { get; set; }
        [JsonProperty("Owner.Name")]
        public string Owner { get; set; }
        public string Resolution { get; set; }
        [JsonProperty("ResolutionReason.Name")]
        public string ResolutionReason { get; set; }
        public DateTime ChangeDate { get; set; }
        public VersionOneAssetState AssetState { get; set; }
        [JsonProperty("_oid")]
        public string Url { get; set; }
        [JsonProperty("CreatedBy.Email")]
        public string CreatedByEmail { get; set; }
    }
}
