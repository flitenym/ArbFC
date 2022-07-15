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
    public class ChainController : BaseController
    {
        private readonly IChainRepository _chainRepository;
        private readonly ILogger<ChainController> _logger;
        public ChainController(IChainRepository chainRepository, ILogger<ChainController> logger)
        {
            _chainRepository = chainRepository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Chain> Get()
        {
            return _chainRepository.Get();
        }
    }
}