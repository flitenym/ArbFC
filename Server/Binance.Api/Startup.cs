using HostLibrary.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Binance.Api.Services;
using Binance.Api.Services.Base;
using Binance.Api.Services.Base.Interfaces;
using Exchange.Common.Services.Base;

namespace Exchange.Common
{
    public class Startup : IModule
    {
        public Task ConfigureAsync(IApplicationBuilder app, IHostApplicationLifetime hal, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            return Task.CompletedTask;
        }

        public Task ConfigureServicesAsync(IServiceCollection services)
        {
            services.AddScoped<IBinanceBaseService, BinanceBaseService>();
            services.AddScoped<ExchangeBaseService, BinanceSpotService>();
            services.AddScoped<ExchangeBaseService, BinanceFuturesService>();

            return Task.CompletedTask;
        }
    }
}