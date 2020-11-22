﻿using AutomationToolkit.Common.Http;
using AutomationToolkit.Confluence.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationToolkit.Confluence
{
    /// <summary>
    /// https://docs.atlassian.com/confluence/REST/latest/
    /// </summary>
    public class ConfluenceRepository
    {
        private string _baseUrl;
        private IHttpService _httpService;

        public ConfluenceRepository(string baseUrl, string apiKey, AuthenticationType authType)//, string user, string password, string domain)
        {
            _baseUrl = baseUrl;
            _httpService = new HttpHostBuilder().HttpService;
            _httpService.AuthenticationToken = apiKey;
            _httpService.AuthType = authType;
        }

        public async Task UpdatePage(string pageId, string htmlContent, string comment)
        {
            //Get ConfluenceConnector Page Info
            var response = await _httpService.GetAsync($"{_baseUrl}/content/{pageId}");
            var wikiPage = JsonConvert.DeserializeObject<ConfluencePage>(response);

            //Update ConfluenceConnector Page
            var requestBody = new
            {
                type = "page",
                title = wikiPage.Title,
                body = new
                {
                    storage = new
                    {
                        value = htmlContent,
                        representation = "storage"
                    }
                },
                version = new
                {
                    number = wikiPage.Version.Number + 1,
                    message = comment
                }
            };

            var requestJson = JsonConvert.SerializeObject(requestBody);

            var result = await _httpService.PutAsync($"{_baseUrl}/content/{pageId}", requestJson);
        }

        public async Task<List<ConfluencePageSearchResult>> SearchContentByLabelAsync(string label)
        {
            //&expand=history
            //expand=metadata.labels
            var response = await _httpService.GetAsync($"{_baseUrl}/content/search?limit=10000&cql=type=page%20AND%20label='{label}'&expand=metadata.labels");
            var wikiPageSearchResults = JsonConvert.DeserializeObject<ConfluencePageSearchResults>(response);
            return wikiPageSearchResults.Results.Where(r => r.Type.Equals("page")).ToList();
        }

        public async Task<List<ConfluencePageSearchResult>> SearchContentByContributor(string username)
        {
            var results = new List<ConfluencePageSearchResult>();
            var response = await _httpService.GetAsync($"{_baseUrl}/content/search?limit=10000&cql=type=page%20and%20(contributor='{username}')&expand=history,history.lastUpdated");
            var wikiPageSearchResults = JsonConvert.DeserializeObject<ConfluencePageSearchResults>(response);
            results.AddRange(wikiPageSearchResults.Results);
            while (wikiPageSearchResults.Links.Next != null)
            {
                response = await _httpService.GetAsync($"{_baseUrl.Replace("/rest/api", "")}{wikiPageSearchResults.Links.Next}");
                wikiPageSearchResults = JsonConvert.DeserializeObject<ConfluencePageSearchResults>(response);
                results.AddRange(wikiPageSearchResults.Results);
            }

            return results.OrderByDescending(r => r.History.LastUpdated.When).ThenBy(r => r.History.CreatedDate).ToList();
        }

        public async Task<List<ConfluencePageSearchResult>> SearchContentByCreator(string username)
        {
            var results = new List<ConfluencePageSearchResult>();
            var response = await _httpService.GetAsync($"{_baseUrl}/content/search?limit=10000&cql=type=page%20and%20(creator='{username}')&expand=history,history.lastUpdated");
            var wikiPageSearchResults = JsonConvert.DeserializeObject<ConfluencePageSearchResults>(response);
            results.AddRange(wikiPageSearchResults.Results);
            while (wikiPageSearchResults.Links.Next != null)
            {
                response = await _httpService.GetAsync($"{_baseUrl.Replace("/rest/api", "")}{wikiPageSearchResults.Links.Next}");
                wikiPageSearchResults = JsonConvert.DeserializeObject<ConfluencePageSearchResults>(response);
                results.AddRange(wikiPageSearchResults.Results);
            }

            return results.OrderByDescending(r => r.History.LastUpdated.When).ThenBy(r => r.History.CreatedDate).ToList();
        }

        public async Task<List<ConfluencePageSearchResult>> SearchContentBySpace(string spaceKey)
        {
            var results = new List<ConfluencePageSearchResult>();
            var response = await _httpService.GetAsync($"{_baseUrl}/content/search?limit=10000&cql=type=page%20and%20space.key='{spaceKey}'%20and%20(created%3E=%222018/12/31%22%20or%20lastModified%3E=%222018/12/31%22)&expand=history,history.lastUpdated");
            var wikiPageSearchResults = JsonConvert.DeserializeObject<ConfluencePageSearchResults>(response);
            results.AddRange(wikiPageSearchResults.Results);
            while (wikiPageSearchResults.Links.Next != null)
            {
                response = await _httpService.GetAsync($"{_baseUrl.Replace("/rest/api", "")}{wikiPageSearchResults.Links.Next}");
                wikiPageSearchResults = JsonConvert.DeserializeObject<ConfluencePageSearchResults>(response);
                results.AddRange(wikiPageSearchResults.Results);
            }

            return results.OrderByDescending(r => r.History.LastUpdated.When).ThenBy(r => r.History.CreatedDate).ToList();
        }

        public async Task<List<ConfluencePageSearchResult>> SearchContentByParentId(string parentId)
        {
            var results = new List<ConfluencePageSearchResult>();
            int counter = 0;
            var confluenceLimit = 1000; //Confluence Max Limit = 1000 pages
            var extendedLimit = confluenceLimit * 20;
            while (counter != extendedLimit)//Workaround to increase limit
            {
                var response = await _httpService.GetAsync($"{_baseUrl}/search?limit={confluenceLimit}&start={counter}&cql=ancestor={parentId}+and+type=page");
                var wikiPageSearchResults = JsonConvert.DeserializeObject<ConfluencePageSearchResults>(response);

                foreach (var result in wikiPageSearchResults.Results)
                {
                    result.Content.History = new ConfluencePageHistory() { LastUpdated = new ConfluencePageLastUpdated() { When = !result.LastModified.HasValue ? DateTime.Now : result.LastModified.Value } };
                    results.Add(result.Content);
                }

                counter = counter + confluenceLimit;
            }

            return results.OrderByDescending(r => r.History.LastUpdated.When).ToList();
        }
    }
}
