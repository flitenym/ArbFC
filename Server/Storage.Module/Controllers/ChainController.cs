using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Storage.Module.Controllers.Base;
using Storage.Module.Controllers.DTO;
using Storage.Module.Entities;
using Storage.Module.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("{id}")]
        public async Task<Chain> GetByIdAsync(long id)
        {
            return await _chainRepository.GetByIdAsync(id);
        }

        [HttpGet("get_by_userId/{userId}")]
        public async Task<IEnumerable<Chain>> GetByUserIdAsync(long userId)
        {
            return await _chainRepository.GetByUserIdAsync(userId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ChainDTO chainDTO)
        {
            (bool isValid, string validMessage) = chainDTO.IsValid();

            if (!isValid)
            {
                return BadRequest(validMessage);
            }

            return StringToResult(
                await _chainRepository
                .CreateAsync(chainDTO)
            );
        }
    }
}