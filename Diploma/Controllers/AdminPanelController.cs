using Diploma.Controllers.CustomAuthAttributes;
using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Diploma.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly AdminPanelBuilder _adminPanelBuilder;

        private const int DEFAULT_COUNT_TAKE_USERS = 10;

        public AdminPanelController(UserRepository userRepository, AdminPanelBuilder adminPanelBuilder)
        {
            _userRepository = userRepository;
            _adminPanelBuilder = adminPanelBuilder;
        }

        [Role(Roles.Admin)]
        public IActionResult Index(int countTakeUsers)
        {
            var users = _userRepository.GetAll().TakeLast(countTakeUsers).ToList();
            var adminPanelViewMode = _adminPanelBuilder.BuildAdminPanelViewModel(users);
            return View(adminPanelViewMode);
        }

        [Role(Roles.Admin)]
        public async Task<IActionResult> EditUser(UserViewModel editedUser)
        {
            var user = await _userRepository.GetByIdAsync(editedUser.Id);
            user.Email = editedUser.Email;
            user.AvatarUrl = editedUser.AvatarUrl;
            user.Username = editedUser.Username;
            user.Birthday = editedUser.Birthday;
            _userRepository.Update(user);
            return RedirectToAction("Index", new { countTakeUsers = DEFAULT_COUNT_TAKE_USERS });
        }
    }
}
