using Diploma.DbStuff.Repositories;
using Diploma.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    public class UserController : Controller
    {
        public readonly UserRepository _userRepository;
        public readonly AuthService _authService;
        public readonly UserBuilder _userBuilder;

        public UserController(UserRepository userRepository, UserBuilder userBuilder, AuthService authService)
        {
            _userRepository = userRepository;
            _userBuilder = userBuilder;
            _authService = authService;
        }

        [Route("user/{id?}")]
        public async Task<IActionResult> Profile(int id)
        {
            if (id == 0)
                id = 3;
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
                }
            }
            return View(userViewModel);
        }
    }
}
