﻿using Newtonsoft.Json;
using System;

namespace AutomationConnectors.Confluence.Model
{
    public class ConfluencePageSearchResult
    {
        public ConfluencePageSearchResult Content { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }

        [JsonProperty("_links")]
        public ConfluencePageLinks Links { get; set; }

        public ConfluencePageHistory History { get; set; }
        public DateTime? LastModified
        {
            get
            {
                return this.History?.LastUpdated.When;
            }
        }
    }
}
