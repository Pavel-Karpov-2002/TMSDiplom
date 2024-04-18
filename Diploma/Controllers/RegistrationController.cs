using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Diploma.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserBuilder _userBuilder;
        private readonly UserRepository _userRepository;
        private readonly RegistrationHelper _registrationHelper;
        private readonly AuthService _authService;

        public RegistrationController(UserBuilder userBuilder, UserRepository userRepository, RegistrationHelper registrationHelper, AuthService authService)
        {
            _userBuilder = userBuilder;
            _userRepository = userRepository;
            _registrationHelper = registrationHelper;
            _authService = authService;
        }

        [Route("registration")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration(RegistrationViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var newUser = _userBuilder.RebuildRegistrationViewToUser(user);
            await _userRepository.AddAsync(newUser);

            _authService.SignInUser(newUser.UserProfile);
            return RedirectToAction("Profile", "User", new { id = newUser.Id });
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyEmail(string email)
        {
            var emailExists = _registrationHelper.IsEmailExists(email);
            return Json(!emailExists);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyLogin(string login)
        {
            var loginExists = _registrationHelper.IsLoginExists(login);
            return Json(!loginExists);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyLoginOrEmailAuthorization(string login)
        {
            var loginExists = _registrationHelper.IsLoginExists(login);
            var emailExists = _registrationHelper.IsEmailExists(login);
            return Json(loginExists || emailExists);
        }
    }
}
