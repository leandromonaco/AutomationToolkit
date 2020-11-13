using AutomationToolkit.AzDevOps;
using AutomationToolkit.Common.Http;
using AutomationToolkit.Confluence;
using AutomationToolkit.SonaType;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutomationToolkit.Test
{
    /// <summary>
    /// https://ossindex.sonatype.org/doc/rest
    /// </summary>
    public class ConfluenceTest
    {
        IConfiguration _configuration;
        ConfluenceRepository _confluenceTestRepository;

        public ConfluenceTest()
        {
            _configuration = new ConfigurationBuilder()
                                        //.SetBasePath(outputPath)
                                        .AddJsonFile("appsettings.json", optional: true)
                                        .AddUserSecrets("cf9af699-d3c2-4090-8231-fd3a1cb45a5f")
                                        .AddEnvironmentVariables()
                                        .Build();

            _confluenceTestRepository = new ConfluenceRepository(_configuration["Confluence:Url"], _configuration["Confluence:Key"], AuthenticationType.Basic);
        }

        [Fact]
        async Task UpdatePage()
        {
            await _confluenceTestRepository.UpdatePage("92374654", "<b>updated from api test 5</b>", "updated from api test 5");
        }
    }
}
