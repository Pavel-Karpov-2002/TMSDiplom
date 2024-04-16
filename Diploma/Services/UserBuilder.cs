﻿using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Diploma.Services.Interfaces;

namespace Diploma.Services
{
    public class UserBuilder : IService
    {
        private readonly FriendBuilder _friendBuilder;
        private readonly FriendRepository _friendRepository;
        private readonly UserProfileBuilder _userProfileBuilder;

        public const string DEFAULT_USER_AVATAR = "/images/userAvatars/default.png";

        public UserBuilder(FriendBuilder friendBuilder, FriendRepository friendRepository, UserProfileBuilder userProfileBuilder)
        {
            _friendBuilder = friendBuilder;
            _friendRepository = friendRepository;
            _userProfileBuilder = userProfileBuilder;
        }

        public UserViewModel RebuildUserToUserViewModel(UserProfile user)
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
                Id = user.UserId,
                Username = user.User.Username,
                Friends = friends,
                Email = user.User.Email ?? "",
                AvatarUrl = user.User.AvatarUrl ?? DEFAULT_USER_AVATAR,
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
