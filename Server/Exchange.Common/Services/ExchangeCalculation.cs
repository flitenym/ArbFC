using Exchange.Common.Services.Base;
using Exchange.Common.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Storage.Module.Classes;
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

        public async Task<(bool IsSuccess, string Message, IEnumerable<TickerInfo> TickersInfo)> GetInterceptTickersAsync(IEnumerable<ExchangeBaseService> exchangeServices)
        {
            List<TickerInfo> tickers = new();

            foreach (ExchangeBaseService exchangeService in exchangeServices)
            {
                (bool isSuccessGetTickers, string messageGetTickers, IEnumerable<TickerInfo> tickersInfo) =
                    await exchangeService.GetTickersAsync();

                if (!isSuccessGetTickers)
                {
                    return (false, messageGetTickers, null);
                }

                if (!tickers.Any())
                {
                    tickers = tickersInfo.ToList();
                }
                else
                {
                    tickers = tickers.Intersect(tickersInfo).ToList();
                }
            }

            return (true, default, tickers);
        }
    }
}