using AutomationToolkit.Common;
using AutomationToolkit.Common.Http;
using AutomationToolkit.Fortify.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutomationToolkit.Fortify
{
    public class FortifyRepository : BaseRepository
    {
        public FortifyRepository(string baseUrl, string apiKey, AuthenticationType authType) : base(baseUrl, apiKey, authType)
        {
        }

        public async Task<string> GetUnifiedLoginTokenAsync()
        {
            //Get ConfluenceConnector Page Info
            var response = await _httpService.PostAsync($"{_baseUrl}/api/v1/tokens", "{\"type\": \"UnifiedLoginToken\"}");
            var authResponse = JsonConvert.DeserializeObject<FortifyTokenResponse>(response);
            return authResponse.Data.Token;
        }

        public async Task<List<FortifyProject>> GetProjectsAsync()
        {
            //Get ConfluenceConnector Page Info
            var response = await _httpService.GetAsync($"{_baseUrl}/api/v1/projects");
            var projectResponse = JsonConvert.DeserializeObject<FortifiyProjectsResponse>(response);
            return projectResponse.Data;
        }

        public async Task<List<FortifyProjectVersion>> GetProjectVersionsAsync(int projectId)
        {
            //Get ConfluenceConnector Page Info
            var response = await _httpService.GetAsync($"{_baseUrl}/api/v1/projects/{projectId}/versions");
            var projectResponse = JsonConvert.DeserializeObject<FortifiyProjectVersionsResponse>(response);
            return projectResponse.Data;
        }

        public async Task<ExportToCsvResponse> ExportToCsvAsync(int projectVersionId, string csvFilename, string filterSet)
        {
            var response = await _httpService.PostAsync($"{_baseUrl}/api/v1/dataExports/action/exportAuditToCsv", $"{{ \"datasetName\": \"Audit\", \"fileName\": \"{csvFilename}\", \"filterSet\": \"{filterSet}\", \"includeCommentsInHistory\": true, \"includeHidden\": true, \"includeRemoved\": true, \"includeSuppressed\": true, \"projectVersionId\": {projectVersionId} }}");
            var projectResponse = JsonConvert.DeserializeObject<ExportToCsvResponse>(response);
            return projectResponse;
        }

        public async Task<List<FortifyDataExport>> GetDataExportsAsync()
        {
            //Get ConfluenceConnector Page Info
            var response = await _httpService.GetAsync($"{_baseUrl}/api/v1/dataExports");
            var projectResponse = JsonConvert.DeserializeObject<FortifyDataExportResponse>(response);
            return projectResponse.Data;
        }

        public async Task<string> GetReportFileTokenAsync()
        {
            //Get ConfluenceConnector Page Info
            var response = await _httpService.PostAsync($"{_baseUrl}/api/v1/fileTokens", "{\"fileTokenType\": \"REPORT_FILE\"}");
            var authResponse = JsonConvert.DeserializeObject<FortifyTokenResponse>(response);
            return authResponse.Data.Token;
        }

        public async Task<string> DownloadCsvAsync(string fileToken, int reportId)
        {
            //Get ConfluenceConnector Page Info
            var response = await _httpService.GetAsync($"{_baseUrl}/transfer/dataExportDownload.html?mat={fileToken}&id={reportId}");
            return response;
        }

        public async Task<List<FortifyIssue>> GetIssuesAsync(int projectVersionId)
        {
            //Get ConfluenceConnector Page Info
            var response = await _httpService.GetAsync($"{_baseUrl}/api/v1/projectVersions/{projectVersionId}/issues?limit=-1");
            var issuesResponse = JsonConvert.DeserializeObject<FortifyIssuesResponse>(response);
            return issuesResponse.Data;
        }

    }
}
