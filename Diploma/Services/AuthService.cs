using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Diploma.Services
{
    public class AuthService : IService
    {
        private readonly UserRepository _userRepository;
        private readonly UserProfileRepository _userProfileRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public const string AUTH_KEY = "keyYEK";

        public AuthService(UserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            UserProfileRepository userProfileRepository)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _userProfileRepository = userProfileRepository;
        }

        public void SignInUser(User user)
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
            _httpContextAccessor
                .HttpContext
                .SignInAsync(AUTH_KEY, principal)
                .Wait();
        }

        public int? GetCurrentUserId()
        {
            var idStr = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            if (idStr == null)
            {
                return null;
            }

            var id = int.Parse(idStr);
            return id;
        }

        public User? GetCurrentUser()
        {
            var id = GetCurrentUserId();
            if (id == null)
            {
                return null;
            }

            return _userRepository.GetById(id.Value);
        }

        public UserProfile? GetCurrentUserProfile()
        {
            var id = GetCurrentUserId();
            if (id == null)
            {
                return null;
            }

            return _userProfileRepository.GetFriendsAndRolesAboutUserById(id.Value);
        }

        public bool IsAuthenticated()
        {
            return GetCurrentUserId() != null;
        }
    }
}
