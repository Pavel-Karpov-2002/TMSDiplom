using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Diploma.Services.Interfaces;

namespace Diploma.Services
{
    public class UserBuilder : IService
    {
        public readonly FriendBuilder _friendBuilder;
        public readonly FriendRepository _friendRepository;

        public UserBuilder(FriendBuilder friendBuilder, FriendRepository friendRepository)
        {
            _friendBuilder = friendBuilder;
            _friendRepository = friendRepository;
        }

        public UserViewModel RebuildUserToUserViewModel(User user)
        {
            var friends = _friendRepository
                .GetFriendsByUserId(user.Id)
                .Select(
                    friend => 
                    _friendBuilder.RebuildFriendToFriendViewModel(friend)
                )
                .ToList();
            return new UserViewModel()
            {
                Id = user.Id,
                Username = user.Username,
                Friends = friends,
                Email = user.Email ?? "",
                IsOnline = user.IsOnline,
                Birthday = user.Birthday ?? null,
                AvatarUrl = user.AvatarUrl ?? ""
            };
        }

        public User BuildUser(string login, string password, string? email, string username, string? avatarUrl, DateTime? birthday)
        {
            return new User()
            {
                Login = login,
                Password = password,
                Email = email ?? "",
                Username = username,
                AvatarUrl = avatarUrl ?? "",
                Birthday = birthday ?? null
            };
        }
    }
}
