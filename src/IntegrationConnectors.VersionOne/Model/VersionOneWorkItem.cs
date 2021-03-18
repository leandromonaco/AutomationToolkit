using Newtonsoft.Json;
using System;

namespace AutomationConnectors.VersionOne.Model
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class VersionOneWorkItem
    {

        [JsonProperty("Super.Number")]
        public string SuperNumber { get; set; }
        [JsonProperty("Super.Name")]
        public string SuperName { get; set; }
        [JsonProperty("Super.Order")]
        public long SuperOrder { get; set; }
        [JsonProperty("Super.Team.Name")]
        public string SuperTeamName { get; set; }
        [JsonProperty("Super.Scope.Name")]
        public string SuperScopeName { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public double? Estimate { get; set; }
        [JsonProperty("Status.Name")]
        public string Status { get; set; }
        [JsonProperty("Team.Name")]
        public string Team { get; set; }
        [JsonProperty("Timebox.Name")]
        public string Sprint { get; set; }
        public long Order { get; set; }
        [JsonProperty("AssetState")]
        public VersionOneAssetState State { get; set; }
        [JsonProperty("ResolutionReason.Name")]
        public object Custom1 { get; set; }
        [JsonProperty("Custom_Severity.Name")]
        public object Custom2 { get; set; }
        [JsonProperty("_oid")]
        public string Url { get; set; }
        public DateTime ChangeDate { get; set; }
        [JsonProperty("CreatedBy.Email")]
        public string CreatedByEmail { get; set; }
        [JsonProperty("Super.Priority.Name")]
        public string SuperPriorityName { get; set; }
    }
}
