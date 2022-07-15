using System.Threading.Tasks;

namespace Storage.Module.Repositories.Base.Interfaces
{
    public interface IBaseRepository
    {
        public Task<(bool IsSuccess, string Message)> SaveChangesAsync();
    }
}
