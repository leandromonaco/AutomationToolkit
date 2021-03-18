using IntegrationConnectors.Common.Http;
using IntegrationConnectors.Fortify;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationConnectors.Test
{
    public class FortifyTest
    {
        IConfiguration _configuration;
        FortifyConnector _fortifyTestRepository;
        FortifyConnector _fortifyTestRepository2;

        public FortifyTest()
        {
            _configuration = new ConfigurationBuilder()
                                        //.SetBasePath(outputPath)
                                        .AddJsonFile("appsettings.json", optional: true)
                                        .AddUserSecrets("cf9af699-d3c2-4090-8231-fd3a1cb45a5f")
                                        .AddEnvironmentVariables()
                                        .Build();

            _fortifyTestRepository = new FortifyConnector(_configuration["Fortify:Url"], _configuration["Fortify:Key"], AuthenticationType.Basic);
        }

        [Fact]
        async Task RunReport()
        {
            var unifiedLoginToken = await _fortifyTestRepository.GetUnifiedLoginTokenAsync();

            _fortifyTestRepository2 = new FortifyConnector(_configuration["Fortify:Url"], unifiedLoginToken, AuthenticationType.FortifyToken);

            var projects = await _fortifyTestRepository2.GetProjectsAsync();

            foreach (var project in projects)
            {
                var versions = await _fortifyTestRepository2.GetProjectVersionsAsync(project.Id);
                foreach (var projectVersion in versions)
                {
                    var issues = await _fortifyTestRepository2.GetIssuesAsync(projectVersion.Id);
                }
            }
        }
    }
}
