using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Module.Entities;
using Storage.Module.Repositories.Base.Interfaces;
using Storage.Module.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Module.Repositories
{
    public class ExchangeChainRepository : IExchangeChainRepository
    {
        private readonly DataContext _dataContext;
        private readonly IBaseRepository _baseRepository;
        private readonly ILogger<ExchangeChainRepository> _logger;
        public ExchangeChainRepository(DataContext dataContext, IBaseRepository baseRepository, ILogger<ExchangeChainRepository> logger)
        {
            _dataContext = dataContext;
            _baseRepository = baseRepository;
            _logger = logger;
        }

        #region Controller methods

        public IEnumerable<ExchangeChain> Get()
        {
            return _dataContext
                .ExchangeChains
                .Include(x => x.Exchange)
                .OrderBy(x => x.Id);
        }

        public Task<(bool IsSuccess, string Message)> SaveChangesAsync()
        {
            return _baseRepository.SaveChangesAsync();
        }

        #endregion
    }
}