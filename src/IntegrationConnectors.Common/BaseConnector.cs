using IntegrationConnectors.Common.Http;
using System;

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
            _httpService.Authenticate(apiKey, authType);
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
    }
}
