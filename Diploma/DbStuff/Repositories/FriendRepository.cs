using Diploma.DbStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace Diploma.DbStuff.Repositories
{
    public class FriendRepository : BaseRepository<Friend>
    {
        public FriendRepository(SocialNetworkWebDbContext context) : base(context) { }

        public void MutualAdditionToFriends(User friend, User friendTo)
        {
            var friendOne = new Friend
            {
                MainUserId = friend.Id,
                FriendOfUser = friendTo
            };
            var friendTwo = new Friend
            {
                MainUserId = friendTo.Id,
                FriendOfUser = friend
            };
            _entyties.Add(friendOne);
            _entyties.Add(friendTwo);
            _context.SaveChanges();
        }

        public async Task MutualAdditionToFriendsAsync(User friend, User friendTo)
        {
            var friendOne = new Friend
            {
                MainUserId = friend.Id,
                FriendOfUser = friendTo
            };
            var friendTwo = new Friend
            {
                MainUserId = friendTo.Id,
                FriendOfUser = friend
            };
            await _entyties.AddAsync(friendOne);
            await _entyties.AddAsync(friendTwo);
            _context.SaveChangesAsync();
        }

        public void RemoveFriend(User me, User friendTo)
        {
            var friendOne = me.Friends.FirstOrDefault(f => f.MainUserId == friendTo.Id);
            var friendTwo = friendTo.Friends.FirstOrDefault(f => f.MainUserId == me.Id);
            _entyties.Remove(friendOne);
            _entyties.Remove(friendTwo);
            _context.SaveChangesAsync();
        }

        public List<Friend>? GetFriendsByUserId(int userId)
        {
            return _entyties
                .Include(friend => friend.FriendOfUser)
                .Where(friends => friends.MainUserId == userId)
                .ToList();
        }
    }
}
