using AutomationToolkit.Common.Http;

namespace AutomationToolkit.Common
{
    public class BaseRepository
    {
        protected IHttpService _httpService;
        protected string _baseUrl;
        
        public BaseRepository(string baseUrl, string apiKey, AuthenticationType authType)
        {
            _baseUrl = baseUrl;
            _httpService = new HttpHostBuilder().HttpService;
            _httpService.Authenticate(apiKey, authType);
        }
    }
}
