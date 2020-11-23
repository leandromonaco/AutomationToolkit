using System;
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

        public void Authenticate(string apiKey, AuthenticationType authType)
        {
            _client = new HttpClient(new HttpClientHandler());

            switch (authType)
            {
                case AuthenticationType.Basic:
                    _client.DefaultRequestHeaders.Add("Authorization", $"Basic {apiKey}");
                    break;
                case AuthenticationType.Bearer:
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                    break;
                case AuthenticationType.DefaultCredentials:
                    _client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
                    break;
                case AuthenticationType.Octopus:
                    _client.DefaultRequestHeaders.Add("X-Octopus-ApiKey", apiKey);
                    break;
                case AuthenticationType.Proget:
                    _client.DefaultRequestHeaders.Add("X-ApiKey", apiKey);
                    break;
                case AuthenticationType.Exchange:
                    //TODO
                    break;
            }
        }

        public async Task<string> GetAsync(string requestUri)
        {
            var result = await _client.GetAsync(requestUri);
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string requestUri, string jsonContent)
        {
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(requestUri, content);
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> PutAsync(string requestUri, string jsonContent)
        {
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var result = await _client.PutAsync(requestUri, content);
            return await result.Content.ReadAsStringAsync();
        }
    }
}
