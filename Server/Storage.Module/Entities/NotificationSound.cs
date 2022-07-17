using System.ComponentModel.DataAnnotations;

namespace Storage.Module.Entities
{
    public class NotificationSound
    {
        [Key]
        public long Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public FileContent FileContent { get; set; }
    }
}