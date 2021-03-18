using AutomationConnectors.Common.Http;
using IntegrationConnectors.Confluence;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationConnectors.Test
{
    /// <summary>
    /// https://ossindex.sonatype.org/doc/rest
    /// </summary>
    public class ConfluenceTest
    {
        IConfiguration _configuration;
        ConfluenceConnector _confluenceTestRepository;

        public ConfluenceTest()
        {
            _configuration = new ConfigurationBuilder()
                                        //.SetBasePath(outputPath)
                                        .AddJsonFile("appsettings.json", optional: true)
                                        .AddUserSecrets("cf9af699-d3c2-4090-8231-fd3a1cb45a5f")
                                        .AddEnvironmentVariables()
                                        .Build();

            _confluenceTestRepository = new ConfluenceConnector(_configuration["Confluence:Url"], _configuration["Confluence:Key"], AuthenticationType.Basic);
        }

        [Fact]
        async Task UpdatePage()
        {
            await _confluenceTestRepository.UpdatePage("92374654", "<b>updated from api test</b>", "updated from api test");
        }


        [Fact]
        async Task SearchPages()
        {
            //var results = await _confluenceTestRepository.SearchContentByLabelAsync("tagged");
            //var results = await _confluenceTestRepository.SearchContentByCreator("username");
            //var results = await _confluenceTestRepository.SearchContentByContributor("username");
            //var results = await _confluenceTestRepository.SearchContentByParentId("26343");
            var results = await _confluenceTestRepository.SearchContentBySpace("SpaceKey");
        }
    }
}
