using AutomationToolkit.Proget;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace AutomationToolkit.Test
{
    public class ProgetTest
    {
        IConfiguration _configuration;
        ProgetRepository _progetRepository;

        public ProgetTest()
        {
            _configuration = new ConfigurationBuilder()
                                        //.SetBasePath(outputPath)
                                        .AddJsonFile("appsettings.json", optional: true)
                                        .AddUserSecrets("cf9af699-d3c2-4090-8231-fd3a1cb45a5f")
                                        .AddEnvironmentVariables()
                                        .Build();

            _progetRepository = new ProgetRepository(_configuration["Proget:Url"], _configuration["Proget:Key"]);
        }

        [Fact]
        async Task GetLastDeployment()
        {
            var packages = await _progetRepository.GetPromotionsAsync(_configuration["Proget:SourceFeed"], _configuration["Proget:TargetFeed"]);
        }
    }
}
