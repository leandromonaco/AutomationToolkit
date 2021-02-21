using AutomationConnectors.AzDevOps.Model.Backlog;

namespace AutomationConnectors.AzDevOps.Model.Repository
{
    public class AzDevOpsCodeRepository
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public AzDevOpsProject Project { get; set; }
    }
}