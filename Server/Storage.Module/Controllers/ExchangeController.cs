using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Storage.Module.Controllers.Base;
using Storage.Module.Entities;
using Storage.Module.Localization;
using Storage.Module.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Module.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : BaseController
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly ILogger<ExchangeController> _logger;
        public ExchangeController(IExchangeRepository exchangeRepository, ILogger<ExchangeController> logger)
        {
            _exchangeRepository = exchangeRepository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Exchange> Get()
        {
            return _exchangeRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<Exchange> GetAsync(long id)
        {
            return await _exchangeRepository.GetByIdAsync(id);
        }

        [HttpGet("get_by_ids")]
        public IActionResult Get([FromBody] IEnumerable<long> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest(StorageLoc.EmptyValues);
            }

            return Ok(_exchangeRepository.GetByIds(ids));
        }
    }
}