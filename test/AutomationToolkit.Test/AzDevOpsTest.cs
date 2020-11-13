using AutomationToolkit.AzDevOps;
using AutomationToolkit.SonaType;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
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

            _azDevOpsTestRepository = new AzDevOpsRepository(_configuration["AzDevOps:Url"], _configuration["AzDevOps:Key"], Common.Http.AuthenticationType.DefaultCredentials);
        }

        [Fact]
        async Task GetRepository()
        {
            //var repository = await _azDevOpsTestRepository.GetRepositoryAsync("Misc");
            //var branches = await _azDevOpsTestRepository.GetBranchesAsync(repository.Id);
            //var commits = await _azDevOpsTestRepository.GetCommitsAsync(repository.Id, "master", new DateTime(2020,1,1), new DateTime(2020, 12, 31));

            //var pendingPRs = await _azDevOpsTestRepository.GetPullRequestsAsync(repository.Id, false);
            //var completePRs = await _azDevOpsTestRepository.GetPullRequestsAsync(repository.Id, true);
            //var prThreads = await _azDevOpsTestRepository.GetPullRequestThreadsAsync(repository.Id, completePRs[0].PullRequestId);

            var buildDefinitions = await _azDevOpsTestRepository.GetBuildDefinitionsAsync(true, true);

                //var usedBuildDefinitions = buildDefinitions.Where(bd => bd.LatestBuild!=null && bd.LatestBuild.QueueTime.Year.Equals(2020)).ToList();
                //var abandonedBuildDefinitions = buildDefinitions.Where(bd => bd.LatestBuild == null || !bd.LatestBuild.QueueTime.Year.Equals(2020)).ToList();

            //var tests = await _azDevOpsTestRepository.GetTestRunsAsync(true);
            var builds = await _azDevOpsTestRepository.GetBuildsAsync();
            var build = await _azDevOpsTestRepository.GetBuildAsync(builds[0].Id);
            await _azDevOpsTestRepository.GetUsersAsync();
        }
    }
}
