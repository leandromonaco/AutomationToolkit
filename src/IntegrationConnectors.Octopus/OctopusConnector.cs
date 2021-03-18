using AutomationConnectors.Common;
using AutomationConnectors.Common.Http;
using IntegrationConnectors.Octopus.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationConnectors.Octopus
{
    public class OctopusConnector : BaseConnector
    {
        private string _spaceId;

        public OctopusConnector(string baseUrl, string apiKey, string spaceId, AuthenticationType authType) : base(baseUrl, apiKey, authType)
        {
            _spaceId = spaceId;
        }

        public async Task<List<OctopusEnvironment>> GetEnvironmentsAsync()
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/{_spaceId}/environments?skip=0&take=2147483647");
            var envs = JsonConvert.DeserializeObject<OctopusEnvironmentResponse>(response);
            return envs.Items;
        }

        public async Task<List<OctopusMachine>> GetMachinesAsync(string environmentId)
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/{_spaceId}/environments/{environmentId}/machines");
            var machines = JsonConvert.DeserializeObject<OctopusMachineResponse>(response);
            return machines.Items;
        }

        public async Task<List<OctopusDeployment>> GetDeploymentsAsync(string machineId)
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/{_spaceId}/machines/{machineId}/tasks");
            var deployments = JsonConvert.DeserializeObject<OctopusDeploymentResponse>(response);
            return deployments.Items;
        }

        public async Task<OctopusProject> GetProjectAsync(string project)
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/{_spaceId}/projects/{project}");
            var proj = JsonConvert.DeserializeObject<OctopusProject>(response);
            return proj;
        }

        public async Task<OctopusVariableSet> GetVariableSetAsync(string ownerId)
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/LibraryVariableSets/{ownerId}");
            var octopusVariableSet = JsonConvert.DeserializeObject<OctopusVariableSet>(response);
            return octopusVariableSet;
        }

        public async Task<List<OctopusVariableSet>> GetVariableSetsAsync(List<string> variableSets)
        {
            var octopusVariableSets = new List<OctopusVariableSet>();
            foreach (var variableSet in variableSets)
            {
                //Spaces-1/variables/
                var response = await _httpService.GetAsync($"{_baseUrl}/{_spaceId}/variables/{variableSet}");
                var octopusVariableSet = JsonConvert.DeserializeObject<OctopusVariableSet>(response);
                octopusVariableSets.Add(octopusVariableSet);
            }

            return octopusVariableSets;
        }

        public async Task<string> GetDeploymentProcessAsync(string deploymentProcessId)
        {
            var response = await _httpService.GetAsync($"{_baseUrl}/{_spaceId}/deploymentprocesses/{deploymentProcessId}");
            return response;
        }

        public async Task UpdateDeploymentProcessAsync(string targetDeploymentProcessId, string jsonContent)
        {
            await _httpService.PutAsync($"{_baseUrl}/{_spaceId}/deploymentprocesses/{targetDeploymentProcessId}", jsonContent);
        }
    }
}