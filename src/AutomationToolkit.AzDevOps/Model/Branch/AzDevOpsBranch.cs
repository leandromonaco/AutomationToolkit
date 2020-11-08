using AutomationToolkit.AzDevOps.Model.Shared;

namespace AutomationToolkit.AzDevOps.Model.Branch
{
    public class AzDevOpsBranch
    {
        public string Name { get; set; }
        public AzDevOpsUser Creator { get; set; }
    }
}
