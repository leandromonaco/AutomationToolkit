using System;
using System.Threading.Tasks;

namespace AutomationConnectors.Common.Http
{
    public interface IHttpService
    {
        Task<string> GetAsync(string requestUri);
        Task<string> PostAsync(string requestUri, string jsonContent);
        Task<string> PutAsync(string requestUri, string jsonContent);
        void Authenticate(string apiKey, AuthenticationType authType);
        TimeSpan Timeout { get; set; }
    }
}