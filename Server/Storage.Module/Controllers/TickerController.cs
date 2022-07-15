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
    public class TickerController : BaseController
    {
        private readonly ITickerRepository _tickerRepository;
        private readonly ILogger<TickerController> _logger;
        public TickerController(ITickerRepository tickerRepository, ILogger<TickerController> logger)
        {
            _tickerRepository = tickerRepository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Ticker> Get()
        {
            return _tickerRepository.Get();
        }
    }
}