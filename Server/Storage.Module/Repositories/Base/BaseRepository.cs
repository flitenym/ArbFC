using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Module.Entities;
using Storage.Module.Localization;
using Storage.Module.Repositories.Base.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Module.Repositories.Base
{
    public class BaseRepository : IBaseRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<BaseRepository> _logger;
        public BaseRepository(DataContext dataContext, ILogger<BaseRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string Message)> SaveChangesAsync()
        {
            try
            {
                await _dataContext.SaveChangesAsync();
                return (true, StorageLoc.SaveSuccess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Join("; ", _dataContext.ChangeTracker.Entries().Select(x => x.Entity.GetType().Name)));
                _dataContext.ChangeTracker.Clear();
                return (false, StorageLoc.SaveUnsuccess);
            }
        }
    }
}