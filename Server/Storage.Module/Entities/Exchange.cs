using System.ComponentModel.DataAnnotations;

namespace Storage.Module.Entities
{
    public class Exchange
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}