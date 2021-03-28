using IntegrationConnectors.Common;
using IntegrationConnectors.Common.Http;
using IntegrationConnectors.Twilio.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationConnectors.Twilio
{
    public class TwilioConnector : BaseConnector
    {
        string _accountSid;
        public TwilioConnector(string baseUrl, string apiKey, AuthenticationType authType) : base(baseUrl, apiKey, authType)
        {
            //Account SID
            byte[] data = Convert.FromBase64String(apiKey);
            string decodedString = Encoding.UTF8.GetString(data);
            _accountSid = decodedString.Split(":")[0];
        }

        public async Task<string> SendMessage(TwilioSMS sms) 
        {
            var parameters = new Dictionary<string, string>
            {
                { "From", sms.From },
                { "To", sms.To },
                { "Body", sms.Body }
            };
            
            var response = await PostWithParametersAsync($"{_baseUrl}/2010-04-01/Accounts/{_accountSid}/Messages.json", parameters);

            return response;
        }
    }
}
