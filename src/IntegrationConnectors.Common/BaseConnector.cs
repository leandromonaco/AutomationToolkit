using IntegrationConnectors.Common.JsonConverters;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntegrationConnectors.Common
{
    public class BaseConnector
    {
        private HttpClient _httpClient;
        protected string _baseUrl;
        protected JsonSerializerOptions _jsonSerializerOptions;

        public BaseConnector(string baseUrl, string apiKey, AuthenticationType authType)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient(new HttpClientHandler());
            if (!authType.Equals(AuthenticationType.None))
            {
                Authenticate(apiKey, authType);
            }

            _jsonSerializerOptions = new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true, 
                MaxDepth = 0,
                Converters = {
                                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
                                new Int64Converter(),
                                new DoubleConverter(),
                                new DateTimeConverter()
                             }
            };
        }

        private TimeSpan _timeout;
        public TimeSpan Timeout
        {
            get => _timeout;
            set
            {
                _timeout = value;
                _httpClient.Timeout = _timeout;
            }
        }

        public void Authenticate(string apiKey, AuthenticationType authType)
        {
            switch (authType)
            {
                case AuthenticationType.Basic:
                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {apiKey}");
                    break;
                case AuthenticationType.Bearer:
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                    break;
                case AuthenticationType.DefaultCredentials:
                    _httpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
                    break;
                case AuthenticationType.OctopusKey:
                    _httpClient.DefaultRequestHeaders.Add("X-Octopus-ApiKey", apiKey);
                    break;
                case AuthenticationType.ProgetKey:
                    _httpClient.DefaultRequestHeaders.Add("X-ApiKey", apiKey);
                    break;
                case AuthenticationType.FortifyToken:
                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"FortifyToken {apiKey}");
                    break;
                default:
                    throw new Exception("AuthenticationType is not implemented");
                    break;
            }
        }

        public async Task<string> GetAsync(string requestUri)
        {
            var result = await _httpClient.GetAsync(requestUri);
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> PostWithJsonAsync(string requestUri, string jsonContent)
        {
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(requestUri, content);
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> PostWithParametersAsync(string requestUri, Dictionary<string, string> parameters)
        {
            var formUrlEncoded = new FormUrlEncodedContent(parameters);
            var result = await _httpClient.PostAsync(requestUri, formUrlEncoded);
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> PutAsync(string requestUri, string jsonContent)
        {
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync(requestUri, content);
            return await result.Content.ReadAsStringAsync();
        }
    }
}
