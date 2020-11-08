using AutomationToolkit.AzDevOps;
using AutomationToolkit.SonaType;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace AutomationToolkit.Test
{
    /// <summary>
    /// https://ossindex.sonatype.org/doc/rest
    /// </summary>
    public class AzDevOpsTest
    {
        IConfiguration _configuration;
        AzDevOpsRepository _azDevOpsTestRepository;

        public AzDevOpsTest()
        {
            _configuration = new ConfigurationBuilder()
                                        //.SetBasePath(outputPath)
                                        .AddJsonFile("appsettings.json", optional: true)
                                        .AddUserSecrets("cf9af699-d3c2-4090-8231-fd3a1cb45a5f")
                                        .AddEnvironmentVariables()
                                        .Build();

            _azDevOpsTestRepository = new AzDevOpsRepository(_configuration["AzDevOps:Url"], _configuration["AzDevOps:Key"]);
        }

        [Fact]
        async Task GetRepository()
        {
            var repository = await _azDevOpsTestRepository.GetRepositoryAsync("Misc");
        }
    }
}
