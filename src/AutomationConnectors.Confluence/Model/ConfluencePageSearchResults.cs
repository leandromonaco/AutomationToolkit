using Newtonsoft.Json;
using System.Collections.Generic;

namespace AutomationConnectors.Confluence.Model
{
    public class ConfluencePageSearchResults
    {
        public List<ConfluencePageSearchResult> Results { get; set; }
        [JsonProperty("_links")]
        public ConfluencePageLinks Links { get; set; }
    }
}