using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Storage.Module.Entities
{
    public class Chain
    {
        [Key]
        public long Id { get; set; }
        public User User { get; set; }
        public virtual ICollection<ExchangeChain> ExchangeChains { get; set; }
        public virtual ICollection<Ticker> Tickers { get; set; }
        /// <summary>
        /// Выбранный цвет
        /// </summary>
        public string SRGB { get; set; }
        /// <summary>
        /// Разница спрэдов
        /// </summary>
        public int Difference { get; set; }
        /// <summary>
        /// Время обработки
        /// </summary>
        public int RefreshTime { get; set; }
        /// <summary>
        /// 24h vol
        /// </summary>
        public long TwentyFourHoursVolume { get; set; }
        /// <summary>
        /// Выбранное уведомление
        /// </summary>
        public NotificationSound NotificationSound { get; set; }
    }
}