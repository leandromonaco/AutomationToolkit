using AutomationConnectors.Common.Http;

namespace AutomationConnectors.Common
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
    }
}
