using Storage.Module.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage.Module.Repositories.Interfaces
{
    public interface ITickerRepository
    {
        public IEnumerable<Ticker> Get();
        public Task<(bool IsSuccess, string Message)> SaveChangesAsync();
    }
}