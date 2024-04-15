using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Services.Interfaces;

namespace Diploma.Services.Permissions
{
    public class AuthirizateUserPermissions : IService
    {
        private readonly AuthService _authService;
        private readonly UserRepository _userRepository;

        public AuthirizateUserPermissions(AuthService authService, UserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        private int? CurrentUserID => _authService.GetCurrentUserId().Value;
        private User? User => _authService.GetCurrentUser();

        public bool CanAddPost(int id)
            => (CurrentUserID is not null && id == CurrentUserID) || 
            User.Roles.Any(r => r.Name.Equals(Roles.Admin.ToString()));

        public bool CanEditPost(int id)
            => (CurrentUserID is not null && id == CurrentUserID) || 
            User.Roles.Any(r => r.Name.Equals(Roles.Admin.ToString())) ||
            User.Roles.Any(r => r.Name.Equals(Roles.Moderator.ToString()));

        public bool CanDeletePost(int id)
            => (CurrentUserID is not null && id == CurrentUserID) ||
            User.Roles.Any(r => r.Name.Equals(Roles.Admin.ToString()));

        public bool CanChangeAvatar(int id)
            => (CurrentUserID is not null && id == CurrentUserID) ||
            User.Roles.Any(r => r.Name.Equals(Roles.Admin.ToString())) ||
            User.Roles.Any(r => r.Name.Equals(Roles.Moderator.ToString()));

        public bool CanAddFriend(int id)
            => _userRepository.GetAllInformationAboutUserById(id).Friends.Any(f => f.MainUserId == CurrentUserID) == false;

        public bool CanOpenAdminPanel()
            => User.Roles.Any(r => r.Name.Equals(Roles.Admin.ToString()));
    }  
}
