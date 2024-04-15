namespace Diploma.Models
{
    public class FriendViewModel
    {
        public string? Username { get; set; }
        public string? AvatarUrl { get; set; }
        public int UserId { get; set; }
        public int MainUserId { get; set; }

        public bool? CanAddThisFriend { get; set; }
    }
}
