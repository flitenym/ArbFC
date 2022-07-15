using Exchange.Common.Classes;
using Exchange.Common.Services.Base;
using Exchange.Common.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Common.Services
{
    public class ExchangeCalculation : IExchangeCalculation
    {
        private readonly ILogger<ExchangeCalculation> _logger;
        public ExchangeCalculation(ILogger<ExchangeCalculation> logger)
        {
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string Message, IEnumerable<AssetInfo>)> GetInterceptCurrenciesAsync(IEnumerable<ExchangeBaseService> exchangeServices)
        {
            List<AssetInfo> assets = new();

            foreach (ExchangeBaseService exchangeService in exchangeServices)
            {
                (bool isSuccessGetCurrencies, string messageGetCurrencies, IEnumerable<AssetInfo> currencies) =
                    await exchangeService.GetCurrenciesAsync();

                if (!isSuccessGetCurrencies)
                {
                    return (false, messageGetCurrencies, null);
                }

                if (!assets.Any())
                {
                    assets = currencies.ToList();
                }
                else
                {
                    assets = assets.Intersect(currencies).ToList();
                }
            }

            return (true, default, assets);
        }
    }
}