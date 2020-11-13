using AutomationToolkit.Common.Http;
using AutomationToolkit.Confluence.Model;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AutomationToolkit.Confluence
{
    /// <summary>
    /// https://docs.atlassian.com/confluence/REST/latest/
    /// </summary>
    public class ConfluenceRepository
    {
        private string _baseUrl;
        private IHttpService _httpService;

        public ConfluenceRepository(string baseUrl, string apiKey, AuthenticationType authType)//, string user, string password, string domain)
        {
            _baseUrl = baseUrl;
            _httpService = new HttpHostBuilder().HttpService;
            _httpService.AuthenticationToken = apiKey;
            _httpService.AuthType = authType;
        }

        public async Task UpdatePage(string pageId, string htmlContent, string comment)
        {
            //Get ConfluenceConnector Page Info
            var response = await _httpService.GetAsync($"{_baseUrl}/content/{pageId}");
            var wikiPage = JsonConvert.DeserializeObject<ConfluencePage>(response);

            //Update ConfluenceConnector Page
            var requestBody = new
            {
                type = "page",
                title = wikiPage.Title,
                body = new
                {
                    storage = new
                    {
                        value = htmlContent,
                        representation = "storage"
                    }
                },
                version = new
                {
                    number = wikiPage.Version.Number + 1,
                    message = comment
                }
            };

            var requestJson = JsonConvert.SerializeObject(requestBody);

            var result = await _httpService.PutAsync($"{_baseUrl}/content/{pageId}", requestJson);
        }
    }
}
