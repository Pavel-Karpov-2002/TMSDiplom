using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Diploma.Services.Interfaces;

namespace Diploma.Services
{
    public class UserBuilder : IService
    {
        private readonly FriendBuilder _friendBuilder;
        private readonly FriendRepository _friendRepository;

        public const string DEFAULT_USER_AVATAR = "/images/userAvatars/default.png";

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
                AvatarUrl = user.AvatarUrl ?? DEFAULT_USER_AVATAR
            };
        }

        public User RebuildRegistrationViewToUser(RegistrationViewModel user)
        {
            return new User()
            {
                Login = user.Login,
                Password = user.Password,
                Email = user.Email ?? "",
                Username = user.Username,
                AvatarUrl = DEFAULT_USER_AVATAR,
                Birthday = null
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
                AvatarUrl = avatarUrl ?? DEFAULT_USER_AVATAR,
                Birthday = birthday ?? null
            };
        }
    }
}
