using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diploma.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserRepository _userRepository;

        public const string AUTH_KEY = "keyYEK";

        public AuthController(UserRepository userRepository)
        {
            _userRepository = userRepository;
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

            SignInUser(user);

            return RedirectToAction("Profile", "User", new { id = user.Id });
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Login", "Auth");
        }

        private void SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("login", user.Login ?? "user"),
                new Claim("username", user.Username ?? "user"),
                new Claim("email", user.Email ?? ""),
                new Claim("avatar", user.AvatarUrl ?? "")
            };

            var identity = new ClaimsIdentity(claims, AUTH_KEY);
            var principal = new ClaimsPrincipal(identity);
            HttpContext
                .SignInAsync(AUTH_KEY, principal)
                .Wait();
        }
    }
}
