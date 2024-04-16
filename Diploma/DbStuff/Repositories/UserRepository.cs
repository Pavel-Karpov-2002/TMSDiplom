using Diploma.DbStuff.Models;
using Diploma.Models;
using Microsoft.EntityFrameworkCore;

namespace Diploma.DbStuff.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(SocialNetworkWebDbContext context) : base(context)
        {
        }

        public bool AnyUserWithLogin(string login)
           => _entyties.Any(x => x.Login == login);

        public bool AnyUserWithEmail(string email)
            => _entyties.Any(x => x.Email == email);

        public User? GetUserByLoginAndPassword(string login, string password)
            => _entyties
            .FirstOrDefault(user => user.Login.Equals(login) && user.Password!.Equals(password));

        public User? GetUserByEmailAndPassword(string email, string password)
            => _entyties
            .FirstOrDefault(user => user.Email.Equals(email) && user.Password!.Equals(password));

        public Task<User?> GetUserByLoginAndPasswordAsync(string login, string password)
            => _entyties
            .FirstOrDefaultAsync(user => user.Login.Equals(login) && user.Password!.Equals(password));

        public User GetUserByEmail(string email)
            => _entyties
            .FirstOrDefault(x => x.Email.Equals(email));

        public Task<User> GetUserByEmailAsync(string email)
            => _entyties
            .FirstOrDefaultAsync(x => x.Email.Equals(email));

        public User GetUserWithUserProfileById(int id)
            => _entyties
            .Include(user => user.UserProfile)
            .FirstOrDefault(x => x.Id == id);

        public async Task<User> GetUserWithUserProfileByIdAsync(int id)
            => await _entyties
            .Include(user => user.UserProfile)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> UpdateAvatarAsync(int userId, string avatarUrl)
        {
            var user = await GetByIdAsync(userId)!;
            user!.AvatarUrl = avatarUrl;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
