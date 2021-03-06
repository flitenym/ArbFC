using HostLibrary.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Storage.Module.Extensions;
using Storage.Module.Repositories.Interfaces;
using Storage.Module.Services.Interfaces;
using Storage.Module.StaticClasses;
using System;
using System.Threading.Tasks;

namespace Storage.Module
{
    public class Startup : IModule
    {
        private IConfiguration _configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ConfigureAsync(IApplicationBuilder app, IHostApplicationLifetime hal, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            var initialCreate = serviceProvider.GetRequiredService<IInitialCreateService>();
            await initialCreate.InitialCreateValuesAsync();

            app.UseWebSockets();
        }

        public Task ConfigureServicesAsync(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Localization");

            services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);

            services.AddStorage(_configuration);
            services.AddStorageServices();
            services.AddStorageRepositoryServices();

            return Task.CompletedTask;
        }
    }
}