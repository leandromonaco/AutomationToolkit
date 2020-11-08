using AutomationToolkit.AzDevOps.Model.Build;
using System.Collections.Generic;

namespace AutomationToolkit.AzDevOps.Model.TestRun
{
    public class AzDevOpsTestRun
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalTests { get; set; }
        public bool IsAutomated { get; set; }
        public string Url { get; set; }
        public AzDevOpsBuild Build { get; set; }
        public List<AzDevOpsTestRunStatsResult> RunStatistics { get; set; }
        public string WebAccessUrl { get; set; }
    }
}