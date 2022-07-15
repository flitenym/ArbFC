using Storage.Module.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage.Module.Repositories.Interfaces
{
    public interface IChainRepository
    {
        public IEnumerable<Chain> Get();
        public Task<(bool IsSuccess, string Message)> SaveChangesAsync();
    }
}