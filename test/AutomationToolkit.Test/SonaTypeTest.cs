using AutomationToolkit.SonaType;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace AutomationToolkit.Test
{
    /// <summary>
    /// https://ossindex.sonatype.org/doc/rest
    /// </summary>
    public class SonaTypeTest
    {
        IConfiguration _configuration;
        SonaTypeRepository _sonaTypeTestRepository;

        public SonaTypeTest()
        {
            _configuration = new ConfigurationBuilder()
                                        //.SetBasePath(outputPath)
                                        .AddJsonFile("appsettings.json", optional: true)
                                        .AddUserSecrets("cf9af699-d3c2-4090-8231-fd3a1cb45a5f")
                                        .AddEnvironmentVariables()
                                        .Build();

            _sonaTypeTestRepository = new SonaTypeRepository(_configuration["SonaType:Url"], _configuration["SonaType:Key"]);
        }

        [Fact]
        async Task ScanComponent()
        {
            var scanResult = await _sonaTypeTestRepository.ScanComponent("{ 	\"coordinates\": [	\"pkg:nuget/jQuery@3.4.1\" 	] }");
        }
    }
}
