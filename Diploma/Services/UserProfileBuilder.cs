using Diploma.DbStuff.Models;
using Diploma.Services.Interfaces;

namespace Diploma.Services
{
    public class UserProfileBuilder : IService
    {
        public UserProfile BuildUserProfile(User user, DateTime birthday)
        {
            return new UserProfile
            {
                User = user,
                Birthday = birthday,
                Friends = new List<Friend>(),
                Roles = new List<Role>(),
                UserId = user.Id
            };
        }
    }
}
