using Exchange.Common.Classes;
using Exchange.Common.Services.Base;
using Exchange.Common.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Storage.Module.Controllers.Base;
using Storage.Module.Localization;
using Storage.Module.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Common.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeCommonController : BaseController
    {
        private readonly IEnumerable<ExchangeBaseService> _exchangeServices;
        private readonly IExchangeCalculation _exchangeCalculation;
        private readonly IExchangeRepository _exchangeRepository;
        private readonly ILogger<ExchangeCommonController> _logger;
        public ExchangeCommonController(
            IEnumerable<ExchangeBaseService> exchangeServices,
            IExchangeCalculation exchangeCalculation,
            IExchangeRepository exchangeRepository,
            ILogger<ExchangeCommonController> logger)
        {
            _exchangeServices = exchangeServices;
            _exchangeCalculation = exchangeCalculation;
            _exchangeRepository = exchangeRepository;
            _logger = logger;
        }

        [HttpGet("get_currencies")]
        public async Task<IActionResult> GetCurrenciesAsync([FromBody] IEnumerable<long> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest(StorageLoc.EmptyValues);
            }

            IEnumerable<Storage.Module.Entities.Exchange> exchanges = _exchangeRepository.GetByIds(ids);

            IEnumerable<string> exchangeNames = exchanges.Select(x => x.Name);

            IEnumerable<ExchangeBaseService> exchangeServices = _exchangeServices.Where(x => exchangeNames.Contains(x.ExchangeName));

            (bool isSuccessGetInterceptCurrencies, string messageGetInterceptCurrencies, IEnumerable<AssetInfo> assets) =
                await _exchangeCalculation.GetInterceptCurrenciesAsync(exchangeServices);

            if (!isSuccessGetInterceptCurrencies)
            {
                return BadRequest(messageGetInterceptCurrencies);
            }

            return Ok(assets);
        }
    }
}