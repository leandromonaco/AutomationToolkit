using AutomationConnectors.AzDevOps.Model.Shared;
using Newtonsoft.Json;
using System;

namespace AutomationConnectors.AzDevOps.Model.Build
{
    public class AzDevOpsBuild
    {
        public string Id { get; set; }
        public string Status { get; set; }
        [JsonProperty("result")]
        public string SubStatus { get; set; }
        public DateTime QueueTime { get; set; }
        public string Parameters { get; set; }
        public AzDevOpsBuildLog Logs { get; set; }
        public AzDevOpsUser RequestedBy { get; set; }
    }
}
