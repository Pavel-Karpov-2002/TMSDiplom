using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Diploma.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diploma.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly AuthService _authService;

        public AuthController(UserRepository userRepository, AuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Profile", "User", new { id = User.FindFirst("id").Value });
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(AuthViewModel authViewModel)
        {
            var user = _userRepository.GetUserByLoginAndPassword(authViewModel.Login, authViewModel.Password);
            
            if (user is null)
            {
                user = _userRepository.GetUserByEmailAndPassword(authViewModel.Login, authViewModel.Password);
            }

            if (user is null)
            {
                ModelState.AddModelError(nameof(AuthViewModel.Login), "Wrong name or passwrod");
                return View(authViewModel);
            }

            _authService.SignInUser(user);

            return RedirectToAction("Profile", "User", new { id = user.Id });
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Login", "Auth");
        }
    }
}
