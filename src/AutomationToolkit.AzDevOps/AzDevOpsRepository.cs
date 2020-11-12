using AutomationToolkit.AzDevOps.Model.Branch;
using AutomationToolkit.AzDevOps.Model.Build;
using AutomationToolkit.AzDevOps.Model.Commit;
using AutomationToolkit.AzDevOps.Model.PullRequest;
using AutomationToolkit.AzDevOps.Model.Repository;
using AutomationToolkit.AzDevOps.Model.TestRun;
using AutomationToolkit.Common.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AutomationToolkit.AzDevOps
{
    public class AzDevOpsRepository
    {
        private string _baseUrl;
        private IHttpService _httpService;

        public AzDevOpsRepository(string baseUrl, string apiKey)
        {
            _baseUrl = baseUrl;
            _httpService = new HttpHostBuilder().HttpService;
            _httpService.AuthenticationToken = apiKey;
        }

        public async Task<List<AzDevOpsCodeRepository>> GetRepositoriesAsync()
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/git/repositories");
            var azureDevOpsRepositories = JsonConvert.DeserializeObject<AzDevOpsCodeRepositories>(response);
            return azureDevOpsRepositories.Value;
        }

        public async Task<AzDevOpsCodeRepository> GetRepositoryAsync(string repositoryName)
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/git/repositories");
            var azureDevOpsRepositories = JsonConvert.DeserializeObject<AzDevOpsCodeRepositories>(response);
            return azureDevOpsRepositories.Value.FirstOrDefault(r => r.Name.Equals(repositoryName));
        }

        public async Task<List<AzDevOpsBranch>> GetBranchesAsync(string repositoryId)
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/git/repositories/{repositoryId}/refs");
            var azureDevOpsBranches = JsonConvert.DeserializeObject<AzDevOpsBranches>(response);
            return azureDevOpsBranches.Value;
        }

        public async Task<List<AzDevOpsCommit>> GetCommitsAsync(string repositoryId, string branchName, DateTime from, DateTime to)
        {
            //Get Commits in the last month
            var startDateStr = from.ToString("MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
            var finishDateStr = to.ToString("MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);

            var response = await _httpService.GetAsync($"{_baseUrl}/git/repositories/{repositoryId}/commits?searchCriteria.itemVersion.version={HttpUtility.UrlEncode(branchName)}&searchCriteria.toDate={finishDateStr}&searchCriteria.fromDate={startDateStr}&searchCriteria.includePushData=true&$top=50");
            var azureDevOpsCommits = JsonConvert.DeserializeObject<AzDevOpsCommits>(response);
            return azureDevOpsCommits.Value;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/rest/api/azure/devops/build/definitions/list?view=azure-devops-rest-6.0
        /// </summary>
        /// <returns></returns>
        public async Task<List<AzDevOpsBuildDefinition>> GetBuildDefinitionsAsync(bool includeAllProperties, bool includeLatestBuilds)
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/build/definitions?includeAllProperties={includeAllProperties}&includeLatestBuilds={includeLatestBuilds}");
            var buildDefinitions = JsonConvert.DeserializeObject<AzDevOpsBuildDefinitions>(response);
            return buildDefinitions.Value;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/rest/api/azure/devops/test/runs/list?view=azure-devops-rest-6.0
        /// </summary>
        /// <returns></returns>
        public async Task<List<AzDevOpsTestRun>> GetTestRunsAsync(bool includeRunDetails)
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/test/runs?includeRunDetails={includeRunDetails}");
            var testRuns = JsonConvert.DeserializeObject<AzDevOpsTestRuns>(response);
            return testRuns.Value;
        }

        public async Task<List<AzDevOpsBuild>> GetBuildsAsync()
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/build/builds");
            var builds = JsonConvert.DeserializeObject<AzDevOpsBuilds>(response);
            return builds.Value;
        }

        public async Task<AzDevOpsBuild> GetBuildAsync(string buildId)
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/build/builds/{buildId}");
            var build = JsonConvert.DeserializeObject<AzDevOpsBuild>(response);
            return build;
        }

        public async Task GetUsersAsync()
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/graph/users");
        }

        public async Task<List<AzDevOpsPullRequest>> GetPullRequestsAsync(string? repositoryId = null, bool? isPullRequestCompleted = null)
        {
            string response;

            if (isPullRequestCompleted.HasValue && isPullRequestCompleted.Value.Equals(true))
            {
                response = await _httpService.GetAsync($"{_baseUrl}/git/repositories/{repositoryId}/pullrequests?searchCriteria.status=completed");
            }
            else if (isPullRequestCompleted.HasValue && isPullRequestCompleted.Value.Equals(false))
            {
                response = await _httpService.GetAsync($"{_baseUrl}/git/repositories/{repositoryId}/pullrequests");
            }
            else
            {
                response = await _httpService.GetAsync($"{_baseUrl}/git/pullrequests");
            }

            return JsonConvert.DeserializeObject<AzDevOpsPullRequests>(response).Value;
        }

        public async Task<List<AzDevOpsPullRequestThread>> GetPullRequestThreadsAsync(string repositoryId, string pullRequestId)
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/git/repositories/{repositoryId}/pullrequests/{pullRequestId}/threads");
            var pullRequestThreads = JsonConvert.DeserializeObject<AzDevOpsPullRequestThreads>(response);
            return pullRequestThreads.Value;
        }


    }
}