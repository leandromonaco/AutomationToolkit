using System.Threading.Tasks;

namespace Octopus.Repository.Http
{
    public interface IHttpService
    {
        Task<string> GetAsync(string requestUri);
        Task<string> PostAsync(string requestUri, string jsonContent);
        Task<string> PutAsync(string requestUri, string jsonContent);

        string AuthenticationToken { get; set; }
    }
}