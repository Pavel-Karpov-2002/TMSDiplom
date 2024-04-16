using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Diploma.Services.Interfaces;

namespace Diploma.Services
{
    public class FriendBuilder : IService
    {
        private readonly UserProfileRepository _userProfileRepository;

        public FriendBuilder(UserProfileRepository userRepository)
        {
            _userProfileRepository = userRepository;
        }

        public FriendViewModel RebuildFriendToFriendViewModel(Friend friend)
        {
            var user = friend.FriendOfUser;
            return new FriendViewModel()
            {
                AvatarUrl = user.User.AvatarUrl,
                UserId = user.UserId,
                Username = user.User.Username
            };
        }

        public Friend RebuildFriendViewModelToFriend(int mainUserId, FriendViewModel friend)
        {
            var user = _userProfileRepository.GetById(friend.UserId);
            return new Friend()
            {
                FriendOfUser = user,
                MainUserId = mainUserId
            };
        }

        public FriendsViewModel BuildFriendsViewModel(UserViewModel user, List<FriendViewModel> friends)
        {
            return new FriendsViewModel
            {
                Friends = friends,
                User = user
            };
        }
    }
}
