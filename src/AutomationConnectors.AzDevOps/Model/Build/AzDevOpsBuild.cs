using AutomationConnectors.AzDevOps.Model.Shared;
using System;

namespace AutomationConnectors.AzDevOps.Model.Build
{
    public class AzDevOpsBuild
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public DateTime QueueTime { get; set; }
        public string Parameters { get; set; }
        public AzDevOpsBuildLog Logs { get; set; }
        public string ReleaseNumber { get; set; }
        public AzDevOpsUser RequestedBy { get; set; }
    }
}
