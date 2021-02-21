using System;
using System.Collections.Generic;

namespace AutomationConnectors.AzDevOps.Model.Backlog
{
    internal class AzDevOpsWorkItemQueryResults
    {
        public string QueryType { get; set; }
        public string QueryResultType { get; set; }
        public DateTime AsOf { get; set; }
        public List<AzDevOpsWorkItemQueryResult> WorkItems { get; set; }
    }
}