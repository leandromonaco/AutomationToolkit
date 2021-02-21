using AutomationConnectors.AzDevOps.Model.Shared;

namespace AutomationConnectors.AzDevOps.Model.Branch
{
    public class AzDevOpsBranch
    {
        public string Name { get; set; }
        public AzDevOpsUser Creator { get; set; }
    }
}
