using System;

namespace AutomationToolkit.AzDevOps.Model.Build
{
    public class AzDevOpsLatestBuild
    {
        public DateTime QueueTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
}