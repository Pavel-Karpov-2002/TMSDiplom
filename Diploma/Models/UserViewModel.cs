namespace Diploma.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsOnline { get; set; }

        public List<FriendViewModel>? Friends { get; set; }
    }
}
