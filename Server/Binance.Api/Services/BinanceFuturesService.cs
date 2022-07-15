using System.Threading.Tasks;
using Storage.Module.Classes;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Storage.Module.Repositories.Interfaces;
using System.Linq;
using Exchange.Common.Classes;
using Binance.Net.Objects.Models.Futures;
using Exchange.Common.Services.Base;
using Binance.Api.Services.Base.Interfaces;
using Storage.Module.StaticClasses;
using Exchange.Common.StaticClasses;

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

        public override async Task<(bool IsSuccess, string Message, IEnumerable<AssetInfo> Currencies)> GetCurrenciesAsync()
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
                .Where(x => ExchangeCurrencies.ToAssetCurrencies.Contains(x.QuoteAsset))
                .Select(x => new AssetInfo(x.BaseAsset, x.QuoteAsset))
            );
        }
    }
}