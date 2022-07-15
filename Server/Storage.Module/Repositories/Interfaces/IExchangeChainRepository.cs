using Storage.Module.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage.Module.Repositories.Interfaces
{
    public interface IExchangeChainRepository
    {
        public IEnumerable<ExchangeChain> Get();
        public Task<(bool IsSuccess, string Message)> SaveChangesAsync();
    }
}