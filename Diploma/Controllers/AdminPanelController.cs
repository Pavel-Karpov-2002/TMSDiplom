using Diploma.Controllers.CustomAuthAttributes;
using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Diploma.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly UserProfileRepository _userProfileRepository;
        private readonly AdminPanelBuilder _adminPanelBuilder;

        private const int DEFAULT_COUNT_TAKE_USERS = 10;

        public AdminPanelController(UserProfileRepository userRepository, AdminPanelBuilder adminPanelBuilder)
        {
            _userProfileRepository = userRepository;
            _adminPanelBuilder = adminPanelBuilder;
        }

        [Authorize]
        [Role(Roles.Admin)]
        public IActionResult Index(int countTakeUsers)
        {
            var users = _userProfileRepository.GetAllUsersWithAllInformations().TakeLast(countTakeUsers).ToList();
            var adminPanelViewMode = _adminPanelBuilder.BuildAdminPanelViewModel(users);
            return View(adminPanelViewMode);
        }

        [Authorize]
        [Role(Roles.Admin)]
        public IActionResult EditUser(EditUserViewModel editedUser)
        {
            var user = _userProfileRepository.GetUserInformationsById(editedUser.Id);
            user.User.Email = editedUser.Email;
            user.User.AvatarUrl = editedUser.AvatarUrl;
            user.User.Username = editedUser.Username;
            user.Birthday = editedUser.Birthday;
            _userProfileRepository.Update(user);
            return RedirectToAction("Index", new { countTakeUsers = DEFAULT_COUNT_TAKE_USERS });
        }
    }
}
