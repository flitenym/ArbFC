using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Storage.Module.Controllers.Base;
using Storage.Module.Entities;
using Storage.Module.Repositories.Interfaces;
using System.Collections.Generic;

namespace Storage.Module.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeChainController : BaseController
    {
        private readonly IExchangeChainRepository _exchangeChainRepository;
        private readonly ILogger<ExchangeChainController> _logger;
        public ExchangeChainController(IExchangeChainRepository exchangeChainRepository, ILogger<ExchangeChainController> logger)
        {
            _exchangeChainRepository = exchangeChainRepository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ExchangeChain> Get()
        {
            return _exchangeChainRepository.Get();
        }
    }
}