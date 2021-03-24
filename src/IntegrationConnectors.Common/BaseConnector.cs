using IntegrationConnectors.Common.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationConnectors.Common
{
    public class BaseConnector
    {
        protected IHttpService _httpService;
        protected string _baseUrl;
        
        public BaseConnector(string baseUrl, string apiKey, AuthenticationType authType)
        {
            _baseUrl = baseUrl;
            _httpService = new HttpHostBuilder().HttpService;
            if (!authType.Equals(AuthenticationType.None))
            {
                _httpService.Authenticate(apiKey, authType);
            }
            
        }

        public BaseConnector()
        {
            _httpService = new HttpHostBuilder().HttpService;
        }

        private TimeSpan _timeout;
        public TimeSpan Timeout
        {
            get => _timeout;
            set
            {
                _timeout = value;
                _httpService.Timeout = _timeout;
            }
        }

        public async Task<string> PostWithJsonAsync(string requestUri, string jsonContent)
        {
            var response = await _httpService.PostWithJsonAsync(requestUri, jsonContent);
            return response;
        }

        public async Task<string> PostWithParametersAsync(string requestUri, Dictionary<string, string> parameters)
        {
            var response = await _httpService.PostWithParametersAsync(requestUri, parameters);
            return response;
        }

        public async Task<string> GetAsync(string url)
        {
            var response = await _httpService.GetAsync(url);
            return response;
        }
    }
}
