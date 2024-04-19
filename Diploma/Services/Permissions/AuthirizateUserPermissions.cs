using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Services.Interfaces;

namespace Diploma.Services.Permissions
{
    public class AuthirizateUserPermissions : IService
    {
        private readonly AuthService _authService;
        private readonly UserRepository _userRepository;
        private readonly UserProfileRepository _userProfileRepository;

        public AuthirizateUserPermissions(AuthService authService, UserRepository userRepository, UserProfileRepository userProfileRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
        }

        private int? CurrentUserID => _authService.GetCurrentUserId();
        private UserProfile? UserProfile => _authService.GetCurrentUserProfile();

        public bool CanAddPost(int id)
            => (CurrentUserID is not null && id == CurrentUserID) ||
            (UserProfile is not null && UserProfile.Roles.Any(r => r.Name.Equals(Roles.Admin.ToString())));

        public bool CanEditPost(int id)
            => (CurrentUserID is not null && id == CurrentUserID) ||
            (UserProfile is not null && 
            (UserProfile.Roles.Any(r => r.Name.Equals(Roles.Admin.ToString())) ||
            UserProfile.Roles.Any(r => r.Name.Equals(Roles.Moderator.ToString()))));

        public bool CanDeletePost(int id)
            => (CurrentUserID is not null && id == CurrentUserID) ||
            (UserProfile is not null && 
            UserProfile.Roles.Any(r => r.Name.Equals(Roles.Admin.ToString())));

        public bool CanChangeAvatar(int id)
            => (CurrentUserID is not null && id == CurrentUserID) ||
            (UserProfile is not null && 
            (UserProfile.Roles.Any(r => r.Name.Equals(Roles.Admin.ToString())) ||
            UserProfile.Roles.Any(r => r.Name.Equals(Roles.Moderator.ToString()))));

        public bool CanAddFriend(int id)
            => _userProfileRepository.GetFriendsUserById(id).Friends?.Any(f => f.MainUserId == CurrentUserID) == false;

        public bool CanOpenAdminPanel()
            => (UserProfile is not null && UserProfile.Roles.Any(r => r.Name.Equals(Roles.Admin.ToString())));
    }  
}
