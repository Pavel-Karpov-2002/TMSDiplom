using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Diploma.Services.Interfaces;

namespace Diploma.Services
{
    public class FriendBuilder : IService
    {
        public readonly UserRepository _userRepository;

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
    }
}
