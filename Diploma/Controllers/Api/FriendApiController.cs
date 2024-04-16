using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers.Api
{
    public class FriendApiController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly UserProfileRepository _userProfileRepository;
        private readonly FriendRepository _friendRepository;

        public FriendApiController(FriendRepository friendRepository, UserRepository userRepository, UserProfileRepository userProfileRepository)
        {
            _friendRepository = friendRepository;
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<bool> AddFriend(FriendViewModel friend)
        {
            var user = await _userRepository.GetUserWithUserProfileByIdAsync(friend.MainUserId);
            var friendTo = await _userRepository.GetUserWithUserProfileByIdAsync(friend.UserId);
            _friendRepository.MutualAdditionToFriends(user.UserProfile, friendTo.UserProfile);
            return true;
        }

        public async Task<bool> DeleteFriend(FriendViewModel friend)
        {
            var user = await _userProfileRepository.GetFriendsUserByIdAsync(friend.MainUserId);
            var friendTo = await _userProfileRepository.GetFriendsUserByIdAsync(friend.UserId);
            _friendRepository.RemoveFriend(user, friendTo);
            return true;
        }
    }
}
