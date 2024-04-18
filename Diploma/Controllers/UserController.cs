using Diploma.Controllers.CustomAuthAttributes;
using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Services;
using Diploma.Services.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly UserProfileRepository _userProfileRepository;
        private readonly FriendRepository _friendRepository;
        private readonly FriendBuilder _friendBuilder;
        private readonly AuthService _authService;
        private readonly UserBuilder _userBuilder;
        private readonly CreateFilePathHelper _createFilePathHelper;
        private readonly UploadFileHelper _uploadFileHelper;
        private readonly AuthirizateUserPermissions _authirizateUserPermissions;

        private string _straightPathForUsers = "";

        private const string DEFAULT_USER_AVATAR_PATH_FOR_DB = "/images/userAvatars/";
        private const string DEFAULT_USER_AVATAR_NAME = "userAvatar_";

        public UserController(UserRepository userRepository,
                              UserBuilder userBuilder,
                              AuthService authService,
                              CreateFilePathHelper createFilePathHelper,
                              UploadFileHelper uploadFileHelper,
                              FriendRepository friendRepository,
                              FriendBuilder friendBuilder,
                              AuthirizateUserPermissions authirizateUserPermissions,
                              UserProfileRepository userProfileRepository)
        {
            _userRepository = userRepository;
            _userBuilder = userBuilder;
            _authService = authService;
            _createFilePathHelper = createFilePathHelper;
            _uploadFileHelper = uploadFileHelper;
            _straightPathForUsers = _createFilePathHelper.GetStraightPath("images", "userAvatars");
            _friendRepository = friendRepository;
            _friendBuilder = friendBuilder;
            _authirizateUserPermissions = authirizateUserPermissions;
            _userProfileRepository = userProfileRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("user/{id:int}")]
        public async Task<IActionResult> Profile(int id)
        {
            var user = await _userProfileRepository.GetAllInformationAboutUserByIdAsync(id);
            var userViewModel = _userBuilder.RebuildUserToUserViewModel(user);
            userViewModel.CanAddPost = _authirizateUserPermissions.CanAddPost(id);
            userViewModel.CanEditPost = _authirizateUserPermissions.CanEditPost(id);
            userViewModel.CanDeletePost = _authirizateUserPermissions.CanDeletePost(id);
            userViewModel.CanChangeAvatar = _authirizateUserPermissions.CanChangeAvatar(id);
            userViewModel.CanAddFriend = _authirizateUserPermissions.CanAddFriend(id);
            userViewModel.CanOpenAdminPanel = _authirizateUserPermissions.CanOpenAdminPanel();
            return View(userViewModel);
        }

        [Route("user/updateUserAvatar")]
        [Authorize]
        [Role(Roles.User, Roles.Moderator, Roles.Admin)]
        public async Task<IActionResult> UpdateUserAvatar(int userId, IFormFile avatar)
        {
            var extension = Path.GetExtension(avatar.FileName);
            var fileName = $"{DEFAULT_USER_AVATAR_NAME}{userId}{extension}";
            var path = _createFilePathHelper.GetCombinePath(_straightPathForUsers, fileName);
            await _uploadFileHelper.UploadFile(path, avatar);
            var urlPath = $"{DEFAULT_USER_AVATAR_PATH_FOR_DB}{fileName}";
            await _userRepository.UpdateAvatarAsync(userId, urlPath);
            return RedirectToAction("Profile", new { id = userId });
        }

        [Authorize]
        public async Task<IActionResult> Friends(int userId)
        {
            var user = await _userProfileRepository.GetAllInformationAboutUserByIdAsync(userId);
            var userViewModel = _userBuilder.RebuildUserToUserViewModel(user);
            userViewModel.CanOpenAdminPanel = _authirizateUserPermissions.CanOpenAdminPanel();
            userViewModel.CanAddFriend = _authirizateUserPermissions.CanAddFriend(userId);
            userViewModel.CanChangeAvatar = _authirizateUserPermissions.CanChangeAvatar(userId);
            var friends = _friendRepository.GetFriendsByUserId(userId);
            var friendViewModels = friends.Select(f => _friendBuilder.RebuildFriendToFriendViewModel(f)).ToList();
            var friendsViewModel = _friendBuilder.BuildFriendsViewModel(userViewModel, friendViewModels);
            return View(friendsViewModel);
        }

        [Authorize]
        public IActionResult SwitchLocale(string locale)
        {
            var userId = _authService.GetCurrentUserId().Value;
            _userProfileRepository.SwitchLocal(userId, locale);
            return RedirectToAction("Profile", new { id = userId });
        }
    }
}
