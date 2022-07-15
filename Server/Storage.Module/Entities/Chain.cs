using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Storage.Module.Entities
{
    public class Chain
    {
        [Key]
        public long Id { get; set; }
        public User User { get; set; }
        public ICollection<ExchangeChain> ExchangeChains { get; set; }
        public ICollection<Ticker> Tickers { get; set; }
    }
}