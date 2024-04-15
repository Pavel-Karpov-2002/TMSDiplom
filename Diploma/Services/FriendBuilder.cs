using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Diploma.Services.Interfaces;

namespace Diploma.Services
{
    public class FriendBuilder : IService
    {
        private readonly UserRepository _userRepository;

        public FriendBuilder(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public FriendViewModel RebuildFriendToFriendViewModel(Friend friend)
        {
            var user = friend.FriendOfUser;
            return new FriendViewModel()
            {
                AvatarUrl = user.AvatarUrl,
                UserId = user.Id,
                Username = user.Username
            };
        }

        public Friend RebuildFriendViewModelToFriend(int mainUserId, FriendViewModel friend)
        {
            var user = _userRepository.GetById(friend.UserId);
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
