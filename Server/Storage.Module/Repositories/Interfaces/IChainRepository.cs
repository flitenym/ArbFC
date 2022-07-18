using Storage.Module.Controllers.DTO;
using Storage.Module.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage.Module.Repositories.Interfaces
{
    public interface IChainRepository
    {
        public IEnumerable<Chain> Get();
        public Task<Chain> GetByIdAsync(long id);
        public Task<IEnumerable<Chain>> GetByUserIdAsync(long userId);
        public Task<(bool IsSuccess, string Message)> CreateAsync(ChainDTO chainDTO);
        public Task<(bool IsSuccess, string Message)> SaveChangesAsync();
    }
}