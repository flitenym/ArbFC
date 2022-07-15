using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Storage.Module.Entities
{
    public class ExchangeChain
    {
        [Key]
        public long Id { get; set; }
        public Exchange Exchange { get; set; }
        public int Order { get; set; }
    }
}