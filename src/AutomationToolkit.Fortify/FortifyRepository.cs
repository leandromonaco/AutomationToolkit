using AutomationToolkit.Common;
using AutomationToolkit.Common.Http;
using AutomationToolkit.Fortify.Model;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AutomationToolkit.Fortify
{
    public class FortifyRepository : BaseRepository
    {
        public FortifyRepository(string baseUrl, string apiKey, AuthenticationType authType) : base(baseUrl, apiKey, authType)
        {
        }

        public async Task<string> GetUnifiedLoginToken()
        {
            //Get ConfluenceConnector Page Info
            var response = await _httpService.PostAsync($"{_baseUrl}/tokens", "{\"type\": \"UnifiedLoginToken\"}");
            var authResponse = JsonConvert.DeserializeObject<FortifyAuthenticationResponse>(response);
            return authResponse.Data.Token;
        }
    }
}
