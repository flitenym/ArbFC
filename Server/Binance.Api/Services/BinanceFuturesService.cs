using Binance.Api.Services.Base.Interfaces;
using Binance.Net.Objects.Models.Futures;
using Exchange.Common.Services.Base;
using Exchange.Common.StaticClasses;
using Microsoft.Extensions.Logging;
using Storage.Module.Classes;
using Storage.Module.Repositories.Interfaces;
using Storage.Module.StaticClasses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Binance.Api.Services
{
    public class BinanceFuturesService : ExchangeBaseService
    {
        private readonly IBinanceBaseService _binanceBaseService;
        private readonly ISettingsRepository _settingsRepository;
        private readonly ILogger<BinanceSpotService> _logger;

        public override string ExchangeName => ExchangeNames.BinanceFutures;

        public BinanceFuturesService(
            IBinanceBaseService binanceBaseService, 
            ISettingsRepository settingsRepository, 
            ILogger<BinanceSpotService> logger)
        {
            _binanceBaseService = binanceBaseService;
            _settingsRepository = settingsRepository;
            _logger = logger;
        }

        public override async Task<(bool IsSuccess, string Message, IEnumerable<TickerInfo> TickersInfo)> GetTickersAsync()
        {
            SettingsInfo settings = await _settingsRepository.GetSettingsAsync();

            (bool isSuccessExchangeInfo, string messageExchangeInfo, BinanceFuturesUsdtExchangeInfo exchangeInfo) =
                await _binanceBaseService.GetFuturesExchangeInfoAsync(settings);

            if (!isSuccessExchangeInfo)
            {
                _logger.LogTrace($"{nameof(BinanceFuturesService)}:{messageExchangeInfo}");
                return (false, messageExchangeInfo, default);
            }

            return (true, default,
                exchangeInfo
                .Symbols
                .Where(x => ExchangeTickers.ToTickers.Contains(x.QuoteAsset))
                .Select(x => new TickerInfo(x.BaseAsset, x.QuoteAsset))
            );
        }
    }
}