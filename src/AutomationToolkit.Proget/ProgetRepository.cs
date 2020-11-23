using AutomationToolkit.Common;
using AutomationToolkit.Common.Http;
using AutomationToolkit.Proget.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutomationToolkit.Proget
{
    public class ProgetRepository : BaseRepository
    {
        public ProgetRepository(string baseUrl, string apiKey, AuthenticationType authType) : base(baseUrl, apiKey, authType)
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
