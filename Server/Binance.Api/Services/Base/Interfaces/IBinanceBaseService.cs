using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;
using Storage.Module.Classes;
using System.Threading.Tasks;

namespace Binance.Api.Services.Base.Interfaces
{
    public interface IBinanceBaseService
    {
        public Task<(bool IsSuccess, string Message, BinanceExchangeInfo ExchangeInfo)> GetSpotExchangeInfoAsync(SettingsInfo settings);
        public Task<(bool IsSuccess, string Message, BinanceFuturesUsdtExchangeInfo ExchangeInfo)> GetFuturesExchangeInfoAsync(SettingsInfo settings);
    }
}