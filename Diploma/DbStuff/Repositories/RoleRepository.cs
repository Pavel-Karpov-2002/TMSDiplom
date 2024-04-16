using Diploma.DbStuff.Models;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;

namespace Diploma.DbStuff.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        private AuthService _authService;

        public RoleRepository(SocialNetworkWebDbContext context, AuthService authService) : base(context)
        {
            _authService = authService;
        }

        public Role GetRoleByName(string roleName)
        {
            return _entyties
                .FirstOrDefault(x => x.Name == roleName);
        }

        public List<Role> GetCurrentUserRoles()
        {
            var currentUser = _authService.GetCurrentUserProfile();
            return GetUserRoles(currentUser);
        }

        public List<Role> GetUserRoles(UserProfile user)
        {
            return _entyties
                .Include(e => e.Users)
                .Where(r => r.Users.Contains(user))
                .ToList();
        }
    }
}
