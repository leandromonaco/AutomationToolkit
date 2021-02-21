using System;

namespace AutomationConnectors.Confluence.Model
{
    public class ConfluencePageLastUpdated
    {
        public ConfluencePageUpdatedBy By { get; set; }
        public DateTime When { get; set; }
    }
}
