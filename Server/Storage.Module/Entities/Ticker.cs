using System.ComponentModel.DataAnnotations;

namespace Storage.Module.Entities
{
    public class Ticker
    {
        [Key]
        public long Id { get; set; }
        public string ToTicker { get; set; }
        public string FromTicker { get; set; }
    }
}