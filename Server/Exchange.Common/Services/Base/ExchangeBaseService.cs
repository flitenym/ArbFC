using Exchange.Common.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Common.Services.Base
{
    public abstract class ExchangeBaseService
    {
        public abstract string ExchangeName { get; }
        /// <summary>
        /// Получение валют
        /// </summary>
        /// <returns></returns>
        public abstract Task<(bool IsSuccess, string Message, IEnumerable<AssetInfo> Currencies)> GetCurrenciesAsync();
    }
}