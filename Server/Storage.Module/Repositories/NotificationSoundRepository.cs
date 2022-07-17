using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Module.Controllers.DTO;
using Storage.Module.Entities;
using Storage.Module.Repositories.Base.Interfaces;
using Storage.Module.Repositories.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Module.Repositories
{
    public class NotificationSoundRepository : INotificationSoundRepository
    {
        private readonly DataContext _dataContext;
        private readonly IBaseRepository _baseRepository;
        private readonly ILogger<NotificationSoundRepository> _logger;
        public NotificationSoundRepository(DataContext dataContext, IBaseRepository baseRepository, ILogger<NotificationSoundRepository> logger)
        {
            _dataContext = dataContext;
            _baseRepository = baseRepository;
            _logger = logger;
        }

        #region Controller methods

        public IEnumerable<NotificationSound> Get()
        {
            return _dataContext
                .NotificationSounds
                .OrderBy(x => x.Id);
        }

        public async Task<NotificationSound> GetByIdAsync(long id)
        {
            return await _dataContext
                .NotificationSounds
                .Include(x => x.FileContent)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<(bool IsSuccess, string Message)> CreateAsync(NotificationSoundDTO notificationSoundDTO)
        {
            NotificationSound notificationSound = new();

            notificationSound.FileName = Path.GetFileNameWithoutExtension(notificationSoundDTO.FileName);
            notificationSound.FileExtension = Path.GetExtension(notificationSoundDTO.FileName);

            FileContent content = new();
            content.Content = notificationSoundDTO.FileContent;

            notificationSound.FileContent = content;

            _dataContext.Add(notificationSound);

            return await SaveChangesAsync();
        }

        public Task<(bool IsSuccess, string Message)> SaveChangesAsync()
        {
            return _baseRepository.SaveChangesAsync();
        }

        #endregion
    }
}