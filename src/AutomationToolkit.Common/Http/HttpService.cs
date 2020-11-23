using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AutomationToolkit.Common.Http
{
    public class HttpService : IHttpService
    {
        HttpClient _client;

        public HttpService(IHttpClientFactory httpClient)
        {

        }

        private void Authenticate()
        {
            _client = new HttpClient(new HttpClientHandler());

            switch (AuthType)
            {
                case AuthenticationType.Basic:
                    _client.DefaultRequestHeaders.Add("Authorization", $"Basic {AuthenticationToken}");
                    break;
                case AuthenticationType.Bearer:
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationToken);
                    break;
                case AuthenticationType.DefaultCredentials:
                    _client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
                    break;
                case AuthenticationType.Octopus:
                    _client.DefaultRequestHeaders.Add("X-Octopus-ApiKey", AuthenticationToken);
                    break;
            }
        }


        public string AuthenticationToken { get; set; }
        public AuthenticationType AuthType { get; set; }

        public async Task<string> GetAsync(string requestUri)
        {
            Authenticate();
            var result = await _client.GetAsync(requestUri);
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string requestUri, string jsonContent)
        {
            Authenticate();
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(requestUri, content);
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> PutAsync(string requestUri, string jsonContent)
        {
            Authenticate();
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var result = await _client.PutAsync(requestUri, content);
            return await result.Content.ReadAsStringAsync();
        }
    }
}
