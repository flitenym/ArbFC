using Storage.Module.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage.Module.Repositories.Interfaces
{
    public interface IExchangeRepository
    {
        public IEnumerable<Exchange> Get();
        public Task<Exchange> GetByIdAsync(long Id);
        public IEnumerable<Exchange> GetByIds(IEnumerable<long> ids);
        public Task<(bool IsSuccess, string Message)> SaveChangesAsync();
    }
}