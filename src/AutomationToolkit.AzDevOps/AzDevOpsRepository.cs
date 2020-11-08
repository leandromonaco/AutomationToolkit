using AutomationToolkit.AzDevOps.Model.Branch;
using AutomationToolkit.AzDevOps.Model.Commit;
using AutomationToolkit.AzDevOps.Model.PullRequest;
using AutomationToolkit.AzDevOps.Model.Repository;
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

        public async Task<List<AzDevOpsPullRequest>> GetPullRequestsAsync(string repositoryId, bool isPullRequestCompleted)
        {
            string response;

            if (isPullRequestCompleted)
            {
                response = await _httpService.GetAsync($"{_baseUrl}/git/repositories/{repositoryId}/pullrequests?searchCriteria.status=completed");
            }
            else
            {
                response = await _httpService.GetAsync($"{_baseUrl}/git/repositories/{repositoryId}/pullrequests");
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