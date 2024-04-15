using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers.Api
{
    public class FriendApiController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly FriendRepository _friendRepository;

        public FriendApiController(FriendRepository friendRepository, UserRepository userRepository)
        {
            _friendRepository = friendRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<bool> AddFriend(FriendViewModel friend)
        {
            var user = await _userRepository.GetByIdAsync(friend.MainUserId);
            var friendTo = await _userRepository.GetByIdAsync(friend.UserId);
            _friendRepository.MutualAdditionToFriends(user, friendTo);
            return true;
        }

        [HttpPost]
        public async Task<bool> DeleteFriend(FriendViewModel friend)
        {
            var user = await _userRepository.GetFriendsUserByIdAsync(friend.MainUserId);
            var friendTo = await _userRepository.GetFriendsUserByIdAsync(friend.UserId);
            _friendRepository.RemoveFriend(user, friendTo);
            return true;
        }
    }
}
