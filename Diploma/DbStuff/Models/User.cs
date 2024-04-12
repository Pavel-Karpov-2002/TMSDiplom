namespace Diploma.DbStuff.Models
{
    public class User : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string Username { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsOnline { get; set; }

        public virtual List<Role>? Roles { get; set; }
        public virtual List<Friend>? Friends { get; set; }
    }
}
