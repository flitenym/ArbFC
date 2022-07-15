using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Module.Entities;
using Storage.Module.Localization;
using Storage.Module.Repositories.Base.Interfaces;
using Storage.Module.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Module.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly IBaseRepository _baseRepository;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(DataContext dataContext, IBaseRepository baseRepository, ILogger<UserRepository> logger)
        {
            _dataContext = dataContext;
            _baseRepository = baseRepository;
            _logger = logger;
        }

        #region Controller methods

        public IEnumerable<User> Get()
        {
            return _dataContext
                .Users
                .OrderBy(x => x.Id);
        }

        public async Task<User> GetByIdAsync(long id)
        {
            return await _dataContext.Users.FindAsync(id);
        }

        public async Task<bool> LoginAsync(User obj)
        {
            return await _dataContext.Users
                .AsNoTracking()
                .Where(x => x.UserName.Equals(obj.UserName))
                .Where(x => x.Password.Equals(obj.Password))
                .AnyAsync();
        }

        public async Task<(bool IsSuccess, string Message)> ChangePasswordAsync(string userName, string oldPassword, string newPassword)
        {
            User user = await _dataContext
                .Users
                .Where(x => x.UserName.Equals(userName))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return (false, string.Format(StorageLoc.NotFoundUserByLogin, userName));
            }

            if (user.Password.Equals(oldPassword))
            {
                if (oldPassword.Equals(newPassword))
                {
                    return (false, StorageLoc.OldAndNewPasswordShouldNotEqual);
                }
                else
                {
                    user.Password = newPassword;

                    _dataContext.Users.Update(user);
                    return await SaveChangesAsync();
                }
            }
            else
            {
                return (false, StorageLoc.OldPasswordIncorrect);
            }
        }

        public async Task<(bool IsSuccess, string Message)> UpdateLanguageAsync(string userName, string language)
        {
            User admin = await _dataContext
                .Users
                .Where(x => x.UserName.Equals(userName))
                .FirstOrDefaultAsync();

            if (admin == null)
            {
                return (false, string.Format(StorageLoc.NotFoundUserByLogin, userName));
            }

            admin.Language = language;

            _dataContext.Users.Update(admin);

            return await SaveChangesAsync();
        }

        public async Task<(bool IsSuccess, string Message)> GetLanguageAsync(string userName)
        {
            User admin = await _dataContext
                .Users
                .AsNoTracking()
                .Where(x => x.UserName.Equals(userName))
                .FirstOrDefaultAsync();

            if (admin == null)
            {
                return (false, string.Format(StorageLoc.NotFoundUserByLogin, userName));
            }

            return (true, admin.Language);
        }

        public Task<(bool IsSuccess, string Message)> CreateAsync(User obj)
        {
            _dataContext.Users.Add(obj);

            return SaveChangesAsync();
        }

        public Task<(bool IsSuccess, string Message)> UpdateAsync(User obj, User newObj)
        {
            obj.UserName = newObj.UserName;
            obj.Password = newObj.Password;
            obj.Language = newObj.Language;

            _dataContext.Users.Update(obj);

            return SaveChangesAsync();
        }

        public Task<(bool IsSuccess, string Message)> DeleteAsync(User obj)
        {
            _dataContext.Users.Remove(obj);

            return SaveChangesAsync();
        }

        public Task<(bool IsSuccess, string Message)> SaveChangesAsync()
        {
            return _baseRepository.SaveChangesAsync();
        }

        #endregion
    }
}