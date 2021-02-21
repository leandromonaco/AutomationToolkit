using Newtonsoft.Json;

namespace AutomationConnectors.AzDevOps.Model.Backlog
{
    public class AzDevOpsWorkItemFields
    {
        private string _team;
        [JsonProperty("System.AreaPath")]
        public string Team
        {
            get { return _team != null ? _team.Replace($"{Project}\\", string.Empty) : string.Empty; }
            set { _team = value; }
        }
        [JsonProperty("System.TeamProject")]
        public string Project { get; set; }
        private string _sprint;
        [JsonProperty("System.IterationPath")]
        public string Sprint
        {
            get { return _sprint != null ? _sprint.Replace($"{Project}\\", string.Empty) : string.Empty; }
            set { _sprint = value; }
        }
        [JsonProperty("System.State")]
        public string Status { get; set; }
        [JsonProperty("System.Title")]
        public string Title { get; set; }
        [JsonProperty("Microsoft.VSTS.Scheduling.Effort")]
        public double Size { get; set; }
        [JsonProperty("Microsoft.VSTS.Common.Priority")]
        public int Priority { get; set; }
    }
}