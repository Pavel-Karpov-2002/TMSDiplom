using System.ComponentModel.DataAnnotations;

namespace Diploma.DbStuff.Models
{
    public class User : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? AvatarUrl { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
