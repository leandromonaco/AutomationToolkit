using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationConnectors.Common.Http
{
    public interface IHttpService
    {
        Task<string> GetAsync(string requestUri);
        Task<string> PostWithJsonAsync(string requestUri, string jsonContent);
        Task<string> PostWithParametersAsync(string requestUri, Dictionary<string, string> parameters);
        Task<string> PutAsync(string requestUri, string jsonContent);
        void Authenticate(string apiKey, AuthenticationType authType);
        TimeSpan Timeout { get; set; }
    }
}