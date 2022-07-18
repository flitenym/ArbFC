using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Storage.Module.Controllers.Base;
using Storage.Module.Controllers.DTO;
using Storage.Module.Entities;
using Storage.Module.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage.Module.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationSoundController : BaseController
    {
        private readonly INotificationSoundRepository _notificationSoundRepository;
        private readonly ILogger<NotificationSoundController> _logger;
        public NotificationSoundController(INotificationSoundRepository notificationSoundRepository, ILogger<NotificationSoundController> logger)
        {
            _notificationSoundRepository = notificationSoundRepository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<NotificationSound> Get()
        {
            return _notificationSoundRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<FileContentResult> GetFileAsync(long id)
        {
            NotificationSound notificationSound = await _notificationSoundRepository.GetByIdAsync(id);
            if (new FileExtensionContentTypeProvider().TryGetContentType(notificationSound.FileExtension, out var contentType))
            {
                return new FileContentResult(notificationSound.FileContent?.Content, contentType);
            }

            return default;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] NotificationSoundDTO notificationSoundDTO)
        {
            (bool isValid, string validMessage) = notificationSoundDTO.IsValid();

            if (!isValid)
            {
                return BadRequest(validMessage);
            }

            return StringToResult(
                await _notificationSoundRepository
                .CreateAsync(notificationSoundDTO)
            );
        }
    }
}