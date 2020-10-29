using AutomationToolkit.Common.Http;
using AutomationToolkit.Proget.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationToolkit.Proget
{
    public class ProgetRepository
    {
        private string _baseUrl;
        private string _apiKey;
        private IHttpService _httpService;

        public ProgetRepository(string baseUrl, string apiKey)
        {
            _baseUrl = baseUrl;
            _apiKey = apiKey;
            _httpService = new HttpHostBuilder().HttpService;
            _httpService.AuthenticationToken = apiKey;
        }

        public async Task<List<ProgetPackage>> GetPromotionsAsync(string sourceFeed, string targetFeed)
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/promotions/list?key={_apiKey}&fromFeed={sourceFeed}&toFeed={targetFeed}");
            var progetPackages = JsonConvert.DeserializeObject<List<ProgetPackage>>(response);
            return progetPackages;
        }

        public async Task PromotePackagesAsync(List<ProgetPackagePromotion> packagePromotions)
        {
            foreach (var packagePromotion in packagePromotions)
            {
                var packagePromotionJson = JsonConvert.SerializeObject(packagePromotion);
                await _httpService.PostAsync($"{_baseUrl}/promotions/promote?key={_apiKey}", packagePromotionJson);
            }
        }
    }
}
