using Exchange.Common.Services;
using Exchange.Common.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Storage.Module.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IExchangeCalculation, ExchangeCalculation>();
        }
    }
}