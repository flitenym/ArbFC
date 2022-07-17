namespace Storage.Module.Controllers.DTO
{
    public class NotificationSoundDTO
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }

        public bool IsValid()
        {
            return
                !string.IsNullOrEmpty(FileName) &&
                FileContent?.Length > 0;
        }
    }
}