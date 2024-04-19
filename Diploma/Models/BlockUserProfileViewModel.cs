namespace Diploma.Models
{
    public class BlockUserProfileViewModel
    {
        public DateTime? Birthday { get; set; }
        public string? Email { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserAvatarUrl { get; set; }

        public bool CanChangeAvatar { get; set; }
        public bool CanAddFriend { get; set; }
    }
}
