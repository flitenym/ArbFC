namespace Storage.Module.Controllers.DTO
{
    public class UserChangePasswordDTO
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}