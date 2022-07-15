using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Storage.Module.Entities
{
    public class Ticker
    {
        [Key]
        public long Id { get; set; }
        public string Asset { get; set; }
    }
}