using Diploma.DbStuff.Repositories;
using Diploma.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    public class UserController : Controller
    {
        public UserRepository _userRepository { get; set; }
        public readonly UserBuilder _userBuilder;

        public UserController(UserRepository userRepository, UserBuilder userBuilder)
        {
            _userRepository = userRepository;
            _userBuilder = userBuilder;
        }

        [Route("user/{id?}")]
        public async Task<IActionResult> Profile(int id)
        {
            var user = await _userRepository.GetAllInformationAboutUserByIdAsync(id);
            var userViewModel = _userBuilder.RebuildUserToUserViewModel(user);
            return View(userViewModel);
        }
    }
}
