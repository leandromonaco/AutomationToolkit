using AutomationToolkit.Fortify;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace AutomationToolkit.Test
{
    public class FortifyTest
    {
        IConfiguration _configuration;
        FortifyRepository _fortifyTestRepository;

        public FortifyTest()
        {
            _configuration = new ConfigurationBuilder()
                                        //.SetBasePath(outputPath)
                                        .AddJsonFile("appsettings.json", optional: true)
                                        .AddUserSecrets("cf9af699-d3c2-4090-8231-fd3a1cb45a5f")
                                        .AddEnvironmentVariables()
                                        .Build();

            _fortifyTestRepository = new FortifyRepository(_configuration["Fortify:Url"], _configuration["Fortify:Key"], Common.Http.AuthenticationType.Basic);
        }

        [Fact]
        async Task GetUnifiedLoginToken()
        {
            var unifiedLoginToken = await _fortifyTestRepository.GetUnifiedLoginToken();
            
        }
    }
}
