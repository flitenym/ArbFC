using System.Threading.Tasks;
using Storage.Module.Classes;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Storage.Module.Repositories.Interfaces;
using System.Linq;
using Exchange.Common.Classes;
using Binance.Api.Services.Interfaces.Base;
using Binance.Net.Objects.Models.Futures;
using Exchange.Common.Services.Base;

namespace Binance.Api.Services
{
    public class BinanceFuturesService : ExchangeBaseService
    {
        private readonly IBinanceBaseService _binanceBaseService;
        private readonly ISettingsRepository _settingsRepository;
        private readonly ILogger<BinanceSpotService> _logger;

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

            (bool isSuccessExchangeInfo, string messageExchangeInfo, BinanceFuturesCoinExchangeInfo spotExchangeInfo) =
                await _binanceBaseService.GetFuturesExchangeInfoAsync(settings);

            if (!isSuccessExchangeInfo)
            {
                return (false, messageExchangeInfo, default);
            }

            return (true, default,
                spotExchangeInfo
                .Symbols
                .Select(x =>
                    new AssetInfo
                    {
                        FromAsset = x.BaseAsset,
                        ToAsset = x.QuoteAsset
                    }
                )
            );
        }
    }
}