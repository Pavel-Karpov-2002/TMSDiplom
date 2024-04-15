using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Services.Interfaces;

namespace Diploma.Services
{
    public class AuthService : IService
    {
        private readonly UserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
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

            return _userRepository.GetAllInformationAboutUserById(id.Value);
        }

        public bool IsAuthenticated()
        {
            return GetCurrentUserId() != null;
        }
    }
}
