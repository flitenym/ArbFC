using Storage.Module.Classes;
using Storage.Module.Localization;
using System.Collections.Generic;
using System.Linq;

namespace Storage.Module.Controllers.DTO
{
    public class ChainDTO
    {
        public long UserId { get; set; }
        public IEnumerable<long> ExchangeIds { get; set; }
        public IEnumerable<AssetInfo> Assets { get; set; }
        public string SRGB { get; set; }
        public int Difference { get; set; }
        public long TwentyFourHoursVolume { get; set; }
        public long? NotificationSoundId { get; set; }

        public (bool IsSuccess, string Message) IsValid()
        {
            if (UserId == 0)
            {
                return (false, string.Format(StorageLoc.EmptyValue, nameof(UserId)));
            }

            if (ExchangeIds == null || !ExchangeIds.Any())
            {
                return (false, string.Format(StorageLoc.EmptyValue, nameof(ExchangeIds)));
            }

            if (Assets == null)
            {
                return (false, string.Format(StorageLoc.EmptyValue, nameof(Assets)));
            }

            if (string.IsNullOrEmpty(SRGB))
            {
                return (false, string.Format(StorageLoc.EmptyValue, nameof(SRGB)));
            }

            if (NotificationSoundId == 0)
            {
                return (false, string.Format(StorageLoc.EmptyValue, nameof(NotificationSoundId)));
            }

            if (Difference == 0)
            {
                return (false, string.Format(StorageLoc.EmptyValue, nameof(Difference)));
            }

            if (TwentyFourHoursVolume < 0)
            {
                return (false, string.Format(StorageLoc.EmptyValue, nameof(TwentyFourHoursVolume)));
            }

            return (true, default);
        }
    }
}