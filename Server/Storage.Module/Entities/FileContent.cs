using System.ComponentModel.DataAnnotations;

namespace Storage.Module.Entities
{
    public class FileContent
    {
        [Key]
        public long Id { get; set; }
        public byte[] Content { get; set; }
    }
}