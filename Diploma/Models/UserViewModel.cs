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

        public bool CanAddPost { get; set; }
        public bool CanEditPost { get; set; }
        public bool CanDeletePost { get; set; }
        public bool CanChangeAvatar { get; set; }
        public bool CanAddFriend { get; set; }

    }
}
