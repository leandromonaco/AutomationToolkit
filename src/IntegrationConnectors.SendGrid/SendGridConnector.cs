using IntegrationConnectors.Common;
using System;

namespace IntegrationConnectors.SendGrid
{
    public class SendGridConnector: BaseConnector
    {
        public SendGridConnector(string baseUrl, string apiKey, AuthenticationType authType) : base(baseUrl, apiKey, authType)
        {

        }
    }
}
