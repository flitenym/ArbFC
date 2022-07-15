using Microsoft.Extensions.Logging;
using Storage.Module.Entities;
using Storage.Module.Repositories.Base.Interfaces;
using Storage.Module.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Module.Repositories
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly DataContext _dataContext;
        private readonly IBaseRepository _baseRepository;
        private readonly ILogger<ExchangeRepository> _logger;
        public ExchangeRepository(DataContext dataContext, IBaseRepository baseRepository, ILogger<ExchangeRepository> logger)
        {
            _dataContext = dataContext;
            _baseRepository = baseRepository;
            _logger = logger;
        }

        #region Controller methods

        public IEnumerable<Exchange> Get()
        {
            return _dataContext
                .Exchanges
                .OrderBy(x => x.Id);
        }

        public async Task<Exchange> GetByIdAsync(long id)
        {
            return await _dataContext.Exchanges.FindAsync(id);
        }

        public IEnumerable<Exchange> GetByIds(IEnumerable<long> ids)
        {
            return _dataContext
                .Exchanges
                .Where(x => ids.Contains(x.Id));
        }

        public Task<(bool IsSuccess, string Message)> SaveChangesAsync()
        {
            return _baseRepository.SaveChangesAsync();
        }

        #endregion
    }
}