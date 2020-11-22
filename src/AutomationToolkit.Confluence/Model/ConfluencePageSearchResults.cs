using Newtonsoft.Json;
using System.Collections.Generic;

namespace AutomationToolkit.Confluence.Model
{
    public class ConfluencePageSearchResults
    {
        public List<ConfluencePageSearchResult> Results { get; set; }
        [JsonProperty("_links")]
        public ConfluencePageLinks Links { get; set; }
    }
}