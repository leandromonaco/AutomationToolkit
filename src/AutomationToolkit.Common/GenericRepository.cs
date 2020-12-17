using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutomationToolkit.Common
{
    public class GenericRepository:BaseRepository
    {

        public async Task<string> PostAsync(string url, string body)
        {
            var response = await _httpService.PostAsync(url, body);
            return response;
        }

        public async Task<string> GetAsync(string url)
        {
            var response = await _httpService.GetAsync(url);
            return response;
        }

    }
}
