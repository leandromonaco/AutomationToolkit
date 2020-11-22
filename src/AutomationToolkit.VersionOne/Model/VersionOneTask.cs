using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AutomationToolkit.VersionOne.Model
{
    public class VersionOneTask
    {
        public string Number { get; set; }
        [JsonProperty("Owners.Email")]
        public List<string> OwnersEmail { get; set; }
        [JsonProperty("Status.Name")]
        public string Status { get; set; }
        public string Name { get; set; }
        [JsonProperty("Parent.Number")]
        public string ParentNumber { get; set; }
        [JsonProperty("Parent.Name")]
        public string ParentName { get; set; }
        [JsonProperty("Parent.Order")]
        public long ParentOrder { get; set; }
        [JsonProperty("AssetState")]
        public VersionOneAssetState State { get; set; }
        public DateTime ChangeDate { get; set; }
        [JsonProperty("_oid")]
        public string Url { get; set; }
    }
}
