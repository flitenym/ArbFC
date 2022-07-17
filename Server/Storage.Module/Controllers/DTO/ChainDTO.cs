using Storage.Module.Classes;
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
        public int RefreshTime { get; set; }
        public long TwentyFourHoursVolume { get; set; }
        public long? NotificationSoundId { get; set; }

        public bool IsValid()
        {
            return
                UserId != 0 &&
                ExchangeIds != null && ExchangeIds.Any() &&
                Assets != null &&
                !string.IsNullOrEmpty(SRGB) &&
                NotificationSoundId != 0 &&
                Difference != 0 &&
                RefreshTime > 10 &&
                TwentyFourHoursVolume >= 0;
        }
    }
}