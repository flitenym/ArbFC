using Binance.Api.Services.Base.Interfaces;
using Binance.Net.Objects.Models.Spot;
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
    public class BinanceSpotService : ExchangeBaseService
    {
        private readonly IBinanceBaseService _binanceBaseService;
        private readonly ISettingsRepository _settingsRepository;
        private readonly ILogger<BinanceSpotService> _logger;

        public override string ExchangeName => ExchangeNames.BinanceSpot;

        public BinanceSpotService(
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

            (bool isSuccessExchangeInfo, string messageExchangeInfo, BinanceExchangeInfo exchangeInfo) =
                await _binanceBaseService.GetSpotExchangeInfoAsync(settings);

            if (!isSuccessExchangeInfo)
            {
                _logger.LogTrace($"{nameof(BinanceSpotService)}:{messageExchangeInfo}");
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