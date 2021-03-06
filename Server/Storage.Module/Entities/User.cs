using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Storage.Module.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Language { get; set; } = DefaultValues.Languages.First();
    }
}