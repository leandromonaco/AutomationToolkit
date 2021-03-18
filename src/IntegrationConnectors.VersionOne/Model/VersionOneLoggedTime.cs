using Newtonsoft.Json;
using System;

namespace AutomationConnectors.VersionOne.Model
{
    public class VersionOneLoggedTime
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        [JsonProperty("Workitem.Number")]
        public string WorkitemNumber { get; set; }
        [JsonProperty("Workitem.Parent.Number")]
        public string WorkitemParentNumber { get; set; }
        [JsonProperty("Workitem.Parent.Super.Number")]
        public string WorkitemSuperNumber { get; set; }
        [JsonProperty("Workitem.Parent.Super.Name")]
        public string WorkitemSuperName { get; set; }
        [JsonProperty("Member.Email")]
        public string MemberEmail { get; set; }
        [JsonProperty("Member.Username")]
        public string MemberUsername { get; set; }
        [JsonProperty("Timebox.Schedule.Name")]
        public string TimeboxScheduleName { get; set; }
        [JsonProperty("Workitem.Parent.Scope.Name")]
        public string WorkitemParentScopeName { get; set; }
    }
}