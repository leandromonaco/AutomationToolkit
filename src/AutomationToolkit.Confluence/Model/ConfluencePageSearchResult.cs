using Newtonsoft.Json;

namespace AutomationToolkit.Confluence.Model
{
    public class ConfluencePageSearchResult
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }

        [JsonProperty("_links")]
        public ConfluenceLinks Links { get; set; }
    }
}
