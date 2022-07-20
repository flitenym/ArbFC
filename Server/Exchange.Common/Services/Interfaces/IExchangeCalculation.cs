using Exchange.Common.Services.Base;
using Storage.Module.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Common.Services.Interfaces
{
    public interface IExchangeCalculation
    {
        public Task<(bool IsSuccess, string Message, IEnumerable<TickerInfo> TickersInfo)> GetInterceptTickersAsync(IEnumerable<ExchangeBaseService> exchangeServices);
    }
}