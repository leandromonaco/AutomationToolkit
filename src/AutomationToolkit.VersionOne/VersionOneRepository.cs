using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AutomationToolkit.VersionOne.Model;
using AutomationToolkit.Common.Http;

namespace AutomationToolkit.VersionOne
{
    public class VersionOneRepository
    {
        private IHttpService _httpService;
        private string _baseUrl;
        private string _apiKey;

        public VersionOneRepository(string baseUrl, string apiKey, AuthenticationType authType)
        {
            _baseUrl = baseUrl;
            _apiKey = apiKey;
            _httpService = new HttpHostBuilder().HttpService;
            _httpService.AuthenticationToken = apiKey;
            _httpService.AuthType = authType;
        }
        public async Task<List<VersionOneWorkItem>> RetrieveDefectsByUserAsync(string email)
        {
            List<VersionOneWorkItem> defects = null;
            var jsonString = @$"{{
                                ""from"": ""Defect"",
                                ""select"": [ 
                                    Number,
                                    Name,
                                    Team.Name,
                                    Status.Name,
                                    AssetState,
                                    Order,
                                    Timebox.Name,
                                    Super.Number,
                                    Estimate,
                                    ResolutionReason.Name,
                                    Custom_Severity.Name,
                                    ChangeDate,
                                    CreatedBy.Email,
                                    Super.Priority.Name
                                ],
                                ""where"": {{
                                    ""CreatedBy.Email"": ""{email}""
                                    }}
                            }}";
            var defectsJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (defectsJson.Length > 10)
            {
                defectsJson = defectsJson.Replace("[\r\n  [\r\n", "[\r\n").Replace("\r\n  ]\r\n]", "\r\n]");
                defects = JsonConvert.DeserializeObject<List<VersionOneWorkItem>>(defectsJson);
            }

            return defects;
        }
        public async Task<List<VersionOneWorkItem>> RetrieveDefectsByTeamAsync(string teamName)
        {
            List<VersionOneWorkItem> defects = null;
            var jsonString = @$"{{
                                ""from"": ""Defect"",
                                ""select"": [ 
                                    Number,
                                    Name,
                                    Team.Name,
                                    Status.Name,
                                    AssetState,
                                    Order,
                                    Timebox.Name,
                                    Super.Number,
                                    Estimate,
                                    ResolutionReason.Name,
                                    Custom_Severity.Name,
                                    ChangeDate,
                                    CreatedBy.Email,
                                    Super.Priority.Name
                                ],
                                ""where"": {{
                                    ""Team.Name"": ""{teamName}""
                                    }}
                            }}";
            var defectsJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (defectsJson.Length > 10)
            {
                defectsJson = defectsJson.Replace("[\r\n  [\r\n", "[\r\n").Replace("\r\n  ]\r\n]", "\r\n]");
                defects = JsonConvert.DeserializeObject<List<VersionOneWorkItem>>(defectsJson);
            }

            return defects;
        }
        public async Task<List<VersionOneWorkItem>> RetrieveWorkItemsByTeamAndStatusAsync(string teamName, string workItemStatus, DateTime asOfDate)
        {
            var asOf = asOfDate.ToString("o");

            //Search Stories
            List<VersionOneWorkItem> stories = null;
            var jsonString = @$"{{
                                ""from"": ""Story"",
                                ""select"": [ 
                                    Number,
                                    Name,
                                    Team.Name,
                                    Status.Name,
                                    AssetState,
                                    Order,
                                    Timebox.Name,
                                    Estimate,
                                    ChangeDate,
                                    Super.Number,
                                    Super.Name,
                                    Super.Order,
                                    Super.Team.Name,
                                    Super.Scope.Name,
                                    ChangeDate,
                                    Super.Priority.Name
                                ],
                                ""where"": {{
                                    ""Status.Name"": ""{workItemStatus}"",
                                    ""Team.Name"": ""{teamName}""
                                    }},
                                ""asof"": ""{asOf}""
                            }}";
            var storiesJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (storiesJson.Length > 10)
            {
                storiesJson = storiesJson.Replace("[\r\n  [\r\n", "[\r\n").Replace("\r\n  ]\r\n]", "\r\n]");
                stories = JsonConvert.DeserializeObject<List<VersionOneWorkItem>>(storiesJson);
            }

            //Search Defects
            List<VersionOneWorkItem> defects = null;
            jsonString = @$"{{
                                ""from"": ""Defect"",
                                ""select"": [ 
                                    Number,
                                    Name,
                                    Team.Name,
                                    Status.Name,
                                    AssetState,
                                    Order,
                                    Timebox.Name,
                                    Estimate,
                                    ResolutionReason.Name,
                                    Custom_Severity.Name,
                                    ChangeDate,
                                    Super.Number,
                                    Super.Name,
                                    Super.Order,
                                    Super.Team.Name,
                                    Super.Scope.Name,
                                    ChangeDate,
                                    Super.Priority.Name
                                ],
                                ""where"": {{
                                    ""Status.Name"": ""{workItemStatus}"",
                                    ""Team.Name"": ""{teamName}""
                                    }},
                                ""asof"": ""{asOf}""
                            }}";
            var defectsJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (defectsJson.Length > 10)
            {
                defectsJson = defectsJson.Replace("[\r\n  [\r\n", "[\r\n").Replace("\r\n  ]\r\n]", "\r\n]");
                defects = JsonConvert.DeserializeObject<List<VersionOneWorkItem>>(defectsJson);
            }

            List<VersionOneWorkItem> workItems = new List<VersionOneWorkItem>();
            workItems.AddRange(stories);
            workItems.AddRange(defects);
            return workItems;
        }
        public async Task<List<VersionOneWorkItem>> RetrieveWorkItemsByTeamAndSprintAsync(string teamName, string sprintId, DateTime asOfDate)
        {
            var asOf = asOfDate.ToString("o");
            //Search Stories
            List<VersionOneWorkItem> stories = null;
            var jsonString = @$"{{
                                ""from"": ""Story"",
                                ""select"": [ 
                                    Number,
                                    Name,
                                    Team.Name,
                                    Status.Name,
                                    AssetState,
                                    Order,
                                    Timebox.Name,
                                    Estimate,
                                    Super.Number,
                                    Super.Name,
                                    Super.Order,
                                    Super.Team.Name,
                                    Super.Scope.Name,
                                    ChangeDate,
                                    Super.Priority.Name
                                ],
                                ""where"": {{
                                    ""Timebox.Name"": ""{sprintId}"",
                                    ""Team.Name"": ""{teamName}""
                                    }},
                                ""asof"": ""{asOf}""
                            }}";
            var storiesJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (storiesJson.Length > 10)
            {
                storiesJson = storiesJson.Replace("[\r\n  [\r\n", "[\r\n").Replace("\r\n  ]\r\n]", "\r\n]");
                stories = JsonConvert.DeserializeObject<List<VersionOneWorkItem>>(storiesJson);
            }

            //Search Defects
            List<VersionOneWorkItem> defects = null;
            jsonString = @$"{{
                                ""from"": ""Defect"",
                                ""select"": [ 
                                    Number,
                                    Name,
                                    Team.Name,
                                    Status.Name,
                                    AssetState,
                                    Order,
                                    Timebox.Name,
                                    Estimate,
                                    Super.Number,
                                    Super.Name,
                                    Super.Order,
                                    Super.Team.Name,
                                    Super.Scope.Name,
                                    ResolutionReason.Name,
                                    Custom_Severity.Name,
                                    ChangeDate,
                                    Super.Priority.Name
                                ],
                                ""where"": {{
                                    ""Timebox.Name"": ""{sprintId}"",
                                    ""Team.Name"": ""{teamName}""
                                    }},
                                ""asof"": ""{asOf}""
                            }}";
            var defectsJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (defectsJson.Length > 10)
            {
                defectsJson = defectsJson.Replace("[\r\n  [\r\n", "[\r\n").Replace("\r\n  ]\r\n]", "\r\n]");
                defects = JsonConvert.DeserializeObject<List<VersionOneWorkItem>>(defectsJson);
            }

            List<VersionOneWorkItem> workItems = new List<VersionOneWorkItem>();
            workItems.AddRange(stories);
            workItems.AddRange(defects);
            return workItems;
        }
        public async Task<List<VersionOneWorkItem>> RetrieveWorkItemsByProjectAsync(string projectName, DateTime asOfDate)
        {
            var asOf = asOfDate.ToString("o");

            //Search Stories
            List<VersionOneWorkItem> stories = null;
            var jsonString = @$"{{
                                ""from"": ""Story"",
                                ""select"": [ 
                                    Number,
                                    Name,
                                    Team.Name,
                                    Status.Name,
                                    AssetState,
                                    Order,
                                    Timebox.Name,
                                    Estimate,
                                    Super.Number,
                                    Super.Name,
                                    Super.Order,
                                    Super.Team.Name,
                                    Super.Scope.Name,
                                    ChangeDate,
                                    Super.Priority.Name
                                ],
                                ""where"": {{
                                    ""Scope.Name"": ""{projectName}""
                                    }},
                                ""asof"": ""{asOf}""
                            }}";
            var storiesJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (storiesJson.Length > 10)
            {
                storiesJson = storiesJson.Replace("[\r\n  [\r\n", "[\r\n").Replace("\r\n  ]\r\n]", "\r\n]");
                stories = JsonConvert.DeserializeObject<List<VersionOneWorkItem>>(storiesJson);
            }

            //Search Defects
            List<VersionOneWorkItem> defects = null;
            jsonString = @$"{{
                                ""from"": ""Defect"",
                                ""select"": [ 
                                    Number,
                                    Name,
                                    Team.Name,
                                    Status.Name,
                                    AssetState,
                                    Order,
                                    Timebox.Name,
                                    Estimate,
                                    Super.Number,
                                    Super.Name,
                                    Super.Order,
                                    Super.Team.Name,
                                    Super.Scope.Name,
                                    ResolutionReason.Name,
                                    Custom_Severity.Name,
                                    ChangeDate,
                                    Super.Priority.Name
                                ],
                                ""where"": {{
                                    ""Scope.Name"": ""{projectName}""
                                    }},
                                ""asof"": ""{asOf}""
                            }}";
            var defectsJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (defectsJson.Length > 10)
            {
                defectsJson = defectsJson.Replace("[\r\n  [\r\n", "[\r\n").Replace("\r\n  ]\r\n]", "\r\n]");
                defects = JsonConvert.DeserializeObject<List<VersionOneWorkItem>>(defectsJson);
            }

            var productWorkItems = new List<VersionOneWorkItem>();
            productWorkItems.AddRange(stories);
            productWorkItems.AddRange(defects);

            return productWorkItems;
        }
        public async Task<List<VersionOneWorkItem>> RetrieveWorkItemsByTeamAsync(string teamName, DateTime asOfDate)
        {
            var asOf = asOfDate.ToString("o");
            //Search Stories
            List<VersionOneWorkItem> stories = null;
            var jsonString = @$"{{
                                ""from"": ""Story"",
                                ""select"": [ 
                                    Number,
                                    Name,
                                    Team.Name,
                                    Status.Name,
                                    AssetState,
                                    Order,
                                    Timebox.Name,
                                    Estimate,
                                    Super.Number,
                                    Super.Name,
                                    Super.Order,
                                    Super.Team.Name,
                                    Super.Scope.Name,
                                    ChangeDate,
                                    Super.Priority.Name
                                ],
                                ""where"": {{
                                    ""Team.Name"": ""{teamName}""
                                    }},
                                ""asof"": ""{asOf}""
                            }}";
            var storiesJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (storiesJson.Length > 10)
            {
                storiesJson = storiesJson.Replace("[\r\n  [\r\n", "[\r\n").Replace("\r\n  ]\r\n]", "\r\n]");
                stories = JsonConvert.DeserializeObject<List<VersionOneWorkItem>>(storiesJson);
            }

            //Search Defects
            List<VersionOneWorkItem> defects = null;
            jsonString = @$"{{
                                ""from"": ""Defect"",
                                ""select"": [ 
                                    Number,
                                    Name,
                                    Team.Name,
                                    Status.Name,
                                    AssetState,
                                    Order,
                                    Timebox.Name,
                                    Estimate,
                                    Super.Number,
                                    Super.Name,
                                    Super.Order,
                                    Super.Team.Name,
                                    Super.Scope.Name,
                                    ResolutionReason.Name,
                                    Custom_Severity.Name,
                                    ChangeDate,
                                    Super.Priority.Name
                                ],
                                ""where"": {{
                                    ""Team.Name"": ""{teamName}""
                                    }},
                                ""asof"": ""{asOf}""
                            }}";
            var defectsJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (defectsJson.Length > 10)
            {
                defectsJson = defectsJson.Replace("[\r\n  [\r\n", "[\r\n").Replace("\r\n  ]\r\n]", "\r\n]");
                defects = JsonConvert.DeserializeObject<List<VersionOneWorkItem>>(defectsJson);
            }

            List<VersionOneWorkItem> workItems = new List<VersionOneWorkItem>();
            workItems.AddRange(stories);
            workItems.AddRange(defects);
            return workItems;
        }
        public async Task<List<VersionOneTask>> RetrieveTasksByTeamAndSprintAsync(string teamName, string sprintId)
        {
            List<VersionOneTask> tasks = null;

            var jsonString = @$"from: Task
select:
    - Number
    - Name
    - Owners.Email
    - Timebox.Schedule.Name
    - Number
    - Parent.Number
    - Parent.Name
    - Parent.Order
    - Team.Name
    - Status.Name
    - AssetState
    - ChangeDate
filter:
  - Timebox.Name='{sprintId}'
  - Team.Name='{teamName}'";

            var tasksJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (tasksJson.Length > 10)
            {
                tasksJson = tasksJson.Replace("[\r\n  [\r\n", "[\r\n").Replace("\r\n  ]\r\n]", "\r\n]");
                tasks = JsonConvert.DeserializeObject<List<VersionOneTask>>(tasksJson);
            }

            return tasks;
        }
        public async Task<List<VersionOneTask>> RetrieveTasksByOwnerAsync(string ownerName)
        {
            List<VersionOneTask> tasks = null;

            var jsonString = @$"from: Task
select:
    - Number
    - Name
    - Owners.Email
    - Timebox.Schedule.Name
    - Number
    - Parent.Number
    - Parent.Name
    - Parent.Order
    - Team.Name
    - Status.Name
    - AssetState
    - ChangeDate
filter:
  - Owners.Name='{ownerName}'";

            var tasksJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (tasksJson.Length > 10)
            {
                tasksJson = tasksJson.Replace("[\r\n  [\r\n", "[\r\n").Replace("\r\n  ]\r\n]", "\r\n]");
                tasks = JsonConvert.DeserializeObject<List<VersionOneTask>>(tasksJson);
              
            }

            return tasks;
        }
        public async Task<List<VersionOneSprint>> RetrieveSprintsAsync(string scheduleName)
        {
            List<VersionOneSprint> sprints = null;
            var jsonString = $@"from: Timebox
select:
- Name
- BeginDate
- EndDate
filter:
- Schedule.Name='{scheduleName}'";

            var sprintsJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (sprintsJson.Length > 10)
            {
                sprintsJson = sprintsJson.Replace("[\r\n  [\r\n", "[\r\n").Replace("\r\n  ]\r\n]", "\r\n]");
                sprints = JsonConvert.DeserializeObject<List<VersionOneSprint>>(sprintsJson);
            }
            return sprints;
        }
        public async Task<List<VersionOneIssue>> RetrieveIssuesByStatusAsync(string status)
        {
            //Status = Active
            List<VersionOneIssue> issues = null;

            //Issues Assigned to Team
            var jsonString = @$"{{
                                    ""from"": ""Issue"",
                                    ""select"": [ 
    	                                Number,
                                        Name,
                                        Team.Name,
                                        IdentifiedBy,
                                        Owner.Name,
                                        Category.Name,
                                        Resolution,
                                        ResolutionReason.Name,
                                        ChangeDate,
                                        AssetState,
                                        CreatedBy.Email
                                    ],
                                    ""where"": {{
                                        ""AssetState"": ""{status}""
                                    }}
                                }}";

            var issuesJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (issuesJson.Length > 10)
            {
                issuesJson = issuesJson.Replace("[\r\n    ", "").Replace("\r\n  ]", "");
                issues = JsonConvert.DeserializeObject<List<VersionOneIssue>>(issuesJson);
            }

            return issues;
        }
        public async Task<List<VersionOneLoggedTime>> RetrieveLoggedTimeByMemberAsync(string memberEmail)
        {
            List<VersionOneLoggedTime> loggedTime = null;
            //Logged time by user
            var jsonString = @$"from: Actual
select:
    - Date
    - Value
    - Timebox.Name
    - Timebox.Schedule.Name
    - Workitem.Number
    - Workitem.Parent.Scope.Name
    - Workitem.Parent.Number
    - Workitem.Parent.Super.Number
    - Workitem.Parent.Super.Name
    - Member.Name
    - Member.Email
    - Member.Username
filter:
  - Member.Email='{memberEmail}'
  - Date>='2020-01-01T00:00:00.0000000'";

            var loggedTimeJson = await _httpService.PostAsync(_baseUrl, jsonString);
            if (loggedTimeJson.Length > 10)
            {
                loggedTimeJson = loggedTimeJson.Replace("[\r\n    ", "").Replace("\r\n  ]", "");
                loggedTime = JsonConvert.DeserializeObject<List<VersionOneLoggedTime>>(loggedTimeJson);
            }

            return loggedTime;
        }
    }
}
