using Storage.Module.Controllers.DTO;
using Storage.Module.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage.Module.Repositories.Interfaces
{
    public interface INotificationSoundRepository
    {
        public IEnumerable<NotificationSound> Get();
        public Task<NotificationSound> GetByIdAsync(long id);
        public Task<(bool IsSuccess, string Message)> CreateAsync(NotificationSoundDTO notificationSoundDTO);
        public Task<(bool IsSuccess, string Message)> SaveChangesAsync();
    }
}