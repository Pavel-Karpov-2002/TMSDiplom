namespace Diploma.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public BlockUserProfileViewModel BlockProfileViewModel { get; set; }
        public List<FriendViewModel>? Friends { get; set; }
        public int CountFriends { get; set; }

        public bool CanAddPost { get; set; }
        public bool CanEditPost { get; set; }
        public bool CanDeletePost { get; set; }
        public bool CanOpenAdminPanel { get; set; }

    }
}
