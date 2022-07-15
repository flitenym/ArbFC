using System.Net;
using System.Threading.Tasks;
using Binance.Net.Clients;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Storage.Module.Classes;
using Microsoft.Extensions.Logging;
using Exchange.Common.Localization;
using Storage.Module.Repositories.Interfaces;
using Binance.Net.Objects.Models.Futures;
using Binance.Api.Services.Base.Interfaces;

namespace Binance.Api.Services.Base
{
    public class BinanceBaseService : IBinanceBaseService
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly ILogger<BinanceBaseService> _logger;

        public BinanceBaseService(ISettingsRepository settingsRepository, ILogger<BinanceBaseService> logger)
        {
            _settingsRepository = settingsRepository;
            _logger = logger;
        }

        #region Base for Binance

        private (BinanceClient client, string Message) GetBinanceClient(SettingsInfo settings)
        {
            if (string.IsNullOrEmpty(settings.BinanceApiKey))
            {
                return (null, string.Format(ExchangeLoc.NotSpecified, nameof(settings.BinanceApiKey)));
            }

            if (string.IsNullOrEmpty(settings.BinanceApiSecret))
            {
                return (null, string.Format(ExchangeLoc.NotSpecified, nameof(settings.BinanceApiSecret)));
            }

            return (new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(settings.BinanceApiKey, settings.BinanceApiSecret)
            }), null);
        }

        private (bool IsSuccess, string Message) CheckStatus<T>(WebCallResult<T> response)
        {
            if (response.ResponseStatusCode == HttpStatusCode.OK)
            {
                return (true, null);
            }
            else
            {
                _logger.LogError(response.Error.ToString());
                return (false, $"{response.Error.Code}: {response.Error.Message}");
            }
        }

        #endregion

        #region Спот

        /// <summary>
        /// Получение системной информации бинанса, включая минимальные значения по валютам
        /// </summary>
        public async Task<(bool IsSuccess, string Message, BinanceExchangeInfo ExchangeInfo)> GetSpotExchangeInfoAsync(SettingsInfo settings)
        {
            var client = GetBinanceClient(settings);

            if (!string.IsNullOrEmpty(client.Message))
            {
                return (false, client.Message, null);
            }

            var result = await client.client.SpotApi.ExchangeData.GetExchangeInfoAsync();

            (bool isSuccessStatus, string messageStatus) = CheckStatus(result);

            if (!isSuccessStatus)
            {
                return (false, messageStatus, null);
            }

            return (true, null, result.Data);
        }

        #endregion

        #region Фьючерс

        /// <summary>
        /// Получение системной информации бинанса, включая минимальные значения по валютам
        /// </summary>
        public async Task<(bool IsSuccess, string Message, BinanceFuturesUsdtExchangeInfo ExchangeInfo)> GetFuturesExchangeInfoAsync(SettingsInfo settings)
        {
            var client = GetBinanceClient(settings);

            if (!string.IsNullOrEmpty(client.Message))
            {
                return (false, client.Message, null);
            }

            var result = await client.client.UsdFuturesApi.ExchangeData.GetExchangeInfoAsync();

            (bool isSuccessStatus, string messageStatus) = CheckStatus(result);

            if (!isSuccessStatus)
            {
                return (false, messageStatus, null);
            }

            return (true, null, result.Data);
        }

        #endregion
    }
}