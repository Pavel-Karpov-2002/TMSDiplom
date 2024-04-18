using Diploma.DbStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace Diploma.DbStuff.Repositories
{
    public class UserProfileRepository : BaseRepository<UserProfile>
    {
        public UserProfileRepository(SocialNetworkWebDbContext context) : base(context)
        {
        }

        public UserProfile GetAllInformationAboutUserByLogin(string login)
            => _entyties
            .Include(user => user.Roles)
            .Include(user => user.Friends)
            .Include(user => user.User)
            .FirstOrDefault(x => x.User.Login == login);

        public async Task<UserProfile> GetAllInformationAboutUserByLoginAsync(string login)
            => await _entyties
            .Include(user => user.Roles)
            .Include(user => user.Friends)
            .Include(user => user.User)
            .FirstOrDefaultAsync(x => x.User.Login == login);

        public UserProfile GetAllInformationAboutUserById(int id)
            => _entyties
            .Include(user => user.Roles)
            .Include(user => user.Friends)
            .Include(user => user.User)
            .FirstOrDefault(x => x.UserId == id);

        public async Task<UserProfile> GetAllInformationAboutUserByIdAsync(int id)
            => await _entyties
            .Include(user => user.Roles)
            .Include(user => user.Friends)
            .Include(user => user.User)
            .FirstOrDefaultAsync(x => x.UserId == id);

        public UserProfile GetFriendsAndRolesAboutUserById(int id)
            => _entyties
            .Include(user => user.Roles)
            .Include(user => user.Friends)
            .FirstOrDefault(x => x.UserId == id);

        public async Task<UserProfile> GetFriendsAndRolesAboutUserByIdAsync(int id)
            => await _entyties
            .Include(user => user.Roles)
            .Include(user => user.Friends)
            .FirstOrDefaultAsync(x => x.UserId == id);

        public UserProfile GetFriendsUserById(int id)
            => _entyties
            .Include(user => user.Friends)
            .FirstOrDefault(x => x.UserId == id);

        public async Task<UserProfile> GetFriendsUserByIdAsync(int id)
            => await _entyties
            .Include(user => user.Friends)
            .FirstOrDefaultAsync(x => x.UserId == id);

        public void SwitchLocal(int userId, string locale)
        {
            var user = _entyties.FirstOrDefault(user => user.UserId == userId);
            user.PreferLocale = locale;
            _context.SaveChanges();
        }
    }
}
