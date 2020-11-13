using AutomationToolkit.Common.Http;
using AutomationToolkit.SonaType.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutomationToolkit.SonaType
{
    public class SonaTypeRepository
    {
        private string _baseUrl;
        private string _apiKey;
        private IHttpService _httpService;

        public SonaTypeRepository(string baseUrl, string apiKey, AuthenticationType authType)
        {
            _baseUrl = baseUrl;
            _apiKey = apiKey;
            _httpService = new HttpHostBuilder().HttpService;
            _httpService.AuthenticationToken = apiKey;
            _httpService.AuthType = authType;
        }

        public async Task<List<SonaTypeComponentScanResult>> ScanComponent(string coordinates)
        {
            var resultJson = await _httpService.PostAsync($"{_baseUrl}/v3/component-report", coordinates);
            var result = JsonConvert.DeserializeObject<List<SonaTypeComponentScanResult>>(resultJson);
            return result;
        }

    }
}
