﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Octopus.Repository.Http
{
    public class HttpHostBuilder
    {
        IHttpService _httpService;

        public HttpHostBuilder()
        {
            var builder = new HostBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddTransient<IHttpService, HttpService>();
                services.AddHttpClient("Http");
            });

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                _httpService = services.GetRequiredService<IHttpService>();
            }
        }

        public IHttpService HttpService { get { return _httpService; } }
    }
}
