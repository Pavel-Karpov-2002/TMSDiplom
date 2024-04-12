using Diploma.DbStuff.Models;

namespace Diploma.DbStuff.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(SocialNetworkWebDbContext context) : base(context)
        {
        }

        public Role GetRoleByName(string roleName)
        {
            return _entyties
                .FirstOrDefault(x => x.Name == roleName);
        }
    }
}
