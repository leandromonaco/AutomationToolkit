using AutomationConnectors.SonaType.Model;
using IntegrationConnectors.Common;
using IntegrationConnectors.Common.Http;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutomationConnectors.SonaType
{
    public class SonaTypeConnector : BaseConnector
    {
        public SonaTypeConnector(string baseUrl, string apiKey, AuthenticationType authType) : base(baseUrl, apiKey, authType)
        {
        }

        public async Task<List<SonaTypeComponentScanResult>> ScanComponent(string coordinates)
        {
            var resultJson = await PostWithJsonAsync($"{_baseUrl}/v3/component-report", coordinates);
            var result = JsonSerializer.Deserialize<List<SonaTypeComponentScanResult>>(resultJson);
            return result;
        }
    }
}
