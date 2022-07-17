using Microsoft.AspNetCore.Http;
using Storage.Module.Localization;

namespace Storage.Module.Controllers.DTO
{
    public class NotificationSoundDTO
    {
        public IFormFile File { get; set; }

        public (bool IsSuccess, string Message) IsValid()
        {
            if (File == null)
            {
                return (false, string.Format(StorageLoc.EmptyValue, nameof(File)));
            }

            if (string.IsNullOrEmpty(File.FileName))
            {
                return (false, string.Format(StorageLoc.EmptyValue, nameof(File.FileName)));
            }

            if (File.Length == 0)
            {
                return (false, string.Format(StorageLoc.EmptyValue, nameof(File.Length)));
            }

            return (true, default);
        }
    }
}