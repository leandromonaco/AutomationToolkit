using AutomationToolkit.Common;
using AutomationToolkit.Common.Http;
using AutomationToolkit.SonaType.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutomationToolkit.SonaType
{
    public class SonaTypeRepository : BaseRepository
    {
        public SonaTypeRepository(string baseUrl, string apiKey, AuthenticationType authType) : base(baseUrl, apiKey, authType)
        {
        }

        public async Task<List<SonaTypeComponentScanResult>> ScanComponent(string coordinates)
        {
            var resultJson = await _httpService.PostAsync($"{_baseUrl}/v3/component-report", coordinates);
            var result = JsonConvert.DeserializeObject<List<SonaTypeComponentScanResult>>(resultJson);
            return result;
        }

    }
}
