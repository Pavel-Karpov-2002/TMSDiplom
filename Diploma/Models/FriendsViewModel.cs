namespace Diploma.Models
{
    public class FriendsViewModel
    {
        public UserViewModel User { get; set; }
        public PaginatorViewModel<FriendViewModel> PaginatorViewModel { get; set; }
        public int CountFriends { get; set; }
    }
}
