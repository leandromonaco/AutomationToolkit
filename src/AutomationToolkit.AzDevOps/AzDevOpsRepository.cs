using AutomationToolkit.AzDevOps.Model.Repository;
using AutomationToolkit.Common.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}