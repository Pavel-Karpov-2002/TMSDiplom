using Diploma.DbStuff.Repositories;
using Diploma.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly FriendRepository _friendRepository;
        private readonly FriendBuilder _friendBuilder;
        private readonly AuthService _authService;
        private readonly UserBuilder _userBuilder;
        private readonly CreateFilePathHelper _createFilePathHelper;
        private readonly UploadFileHelper _uploadFileHelper;

        private string _straightPathForUsers = "";

        private const string DEFAULT_USER_AVATAR_PATH_FOR_DB = "/images/userAvatars/";
        private const string DEFAULT_USER_AVATAR_NAME = "userAvatar_";

        public UserController(UserRepository userRepository, UserBuilder userBuilder, AuthService authService, CreateFilePathHelper createFilePathHelper, UploadFileHelper uploadFileHelper, FriendRepository friendRepository, FriendBuilder friendBuilder)
        {
            _userRepository = userRepository;
            _userBuilder = userBuilder;
            _authService = authService;
            _createFilePathHelper = createFilePathHelper;
            _uploadFileHelper = uploadFileHelper;
            _straightPathForUsers = _createFilePathHelper.GetStraightPath("images", "userAvatars");
            _friendRepository = friendRepository;
            _friendBuilder = friendBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("user/{id:int}")]
        public async Task<IActionResult> Profile(int id)
        {
            var user = await _userRepository.GetAllInformationAboutUserByIdAsync(id);
            var userViewModel = _userBuilder.RebuildUserToUserViewModel(user);

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = _authService.GetCurrentUserId().Value;
                if (userId == id)
                {
                    userViewModel.CanAddPost = true;
                    userViewModel.CanEditPost = true;
                    userViewModel.CanDeletePost = true;
                    userViewModel.CanChangeAvatar = true;
                }
                else
                {
                    var isFriend = user.Friends.Any(f => f.MainUserId == userId);
                    if (!isFriend)
                    {
                        userViewModel.CanAddFriend = true;
                    }
                }
            }
            return View(userViewModel);
        }

        [Route("user/updateUserAvatar")]
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

        public async Task<IActionResult> Friends(int userId)
        {
            var user = await _userRepository.GetFriendsUserByIdAsync(userId);
            var userViewModel = _userBuilder.RebuildUserToUserViewModel(user);
            var friends = _friendRepository.GetFriendsByUserId(userId);
            var friendViewModels = friends.Select(f => _friendBuilder.RebuildFriendToFriendViewModel(f)).ToList();
            var friendsViewModel = _friendBuilder.BuildFriendsViewModel(userViewModel, friendViewModels);
            return View(friendsViewModel);
        }
    }
}
