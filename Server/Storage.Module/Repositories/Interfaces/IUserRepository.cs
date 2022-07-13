using Storage.Module.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage.Module.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable<User> Get();
        public Task<User> GetByIdAsync(long Id);
        public Task<bool> LoginAsync(User obj);
        public Task<(bool IsSuccess, string Message)> ChangePasswordAsync(string userName, string oldPassword, string newPassword);
        public Task<(bool IsSuccess, string Message)> UpdateLanguageAsync(string userName, string language);
        public Task<(bool IsSuccess, string Message)> GetLanguageAsync(string userName);
        public Task<(bool IsSuccess, string Message)> CreateAsync(User obj);
        public Task<(bool IsSuccess, string Message)> UpdateAsync(User obj, User newObj);
        public Task<(bool IsSuccess, string Message)> DeleteAsync(User obj);
        public Task<(bool IsSuccess, string Message)> SaveChangesAsync();
    }
}