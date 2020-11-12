using System;

namespace AutomationToolkit.AzDevOps.Model.Build
{
    public class AzDevOpsBuildDefinition
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public AzDevOpsLatestBuild LatestBuild { get; set; }
        public string QueueStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
