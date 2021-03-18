using IntegrationConnectors.Common;
using IntegrationConnectors.Common.Http;
using IntegrationConnectors.Proget.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationConnectors.Proget
{
    public class ProgetConnector : BaseConnector
    {
        public ProgetConnector(string baseUrl, string apiKey, AuthenticationType authType) : base(baseUrl, apiKey, authType)
        {
        }

        public async Task<List<ProgetPackage>> GetPromotionsAsync(string sourceFeed, string targetFeed)
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/promotions/list?fromFeed={sourceFeed}&toFeed={targetFeed}");
            var progetPackages = JsonConvert.DeserializeObject<List<ProgetPackage>>(response);
            return progetPackages;
        }

        public async Task PromotePackagesAsync(List<ProgetPackagePromotion> packagePromotions)
        {
            foreach (var packagePromotion in packagePromotions)
            {
                var packagePromotionJson = JsonConvert.SerializeObject(packagePromotion);
                await _httpService.PostAsync($"{_baseUrl}/promotions/promote", packagePromotionJson);
            }
        }
    }
}
