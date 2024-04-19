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

        public UserViewModel RebuildUserToUserViewModel(UserProfile user)
        {
            var friends = _friendRepository
                .GetFriendsByUserId(user.UserId)
                .Select(
                    friend => 
                    _friendBuilder.RebuildFriendToFriendViewModel(friend)
                )
                .ToList();
            return new UserViewModel()
            {
                Id = user.UserId,
                Friends = friends,
                BlockProfileViewModel = new BlockUserProfileViewModel
                {
                    UserId = user.UserId,
                    Username = user.User.Username,
                    Email = user.User.Email ?? "",
                    UserAvatarUrl = user.User.AvatarUrl ?? DEFAULT_USER_AVATAR,
                    Birthday = user.Birthday
                },
                CountFriends = friends.Count()
            };
        }

        public BlockUserProfileViewModel BuildBlockProfileViewModel(UserProfile user)
        {
            return new BlockUserProfileViewModel
            {
                UserId = user.UserId,
                Username = user.User.Username,
                Email = user.User.Email ?? "",
                UserAvatarUrl = user.User.AvatarUrl ?? DEFAULT_USER_AVATAR,
                Birthday = user.Birthday
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
                UserProfile = new UserProfile
                {
                    Friends = new (),
                    Roles = new ()
                }
            };
        }

        public User BuildUser(string login, string password, string? email, string username, string? avatarUrl)
        {
            return new User()
            {
                Login = login,
                Password = password,
                Email = email ?? "",
                Username = username,
                AvatarUrl = avatarUrl ?? DEFAULT_USER_AVATAR,
                UserProfile = new UserProfile
                {
                    Friends = new(),
                    Roles = new()
                }
            };
        }
    }
}
