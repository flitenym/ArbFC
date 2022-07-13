using System.Threading.Tasks;
using Binance.Net.Objects.Models.Spot;
using Storage.Module.Classes;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Storage.Module.Repositories.Interfaces;
using System.Linq;
using Exchange.Common.Classes;
using Binance.Api.Services.Interfaces.Base;
using Exchange.Common.Services.Base;

namespace Binance.Api.Services
{
    public class BinanceSpotService : ExchangeBaseService
    {
        private readonly IBinanceBaseService _binanceBaseService;
        private readonly ISettingsRepository _settingsRepository;
        private readonly ILogger<BinanceSpotService> _logger;

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

            (bool isSuccessExchangeInfo, string messageExchangeInfo, BinanceExchangeInfo spotExchangeInfo) =
                await _binanceBaseService.GetSpotExchangeInfoAsync(settings);

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