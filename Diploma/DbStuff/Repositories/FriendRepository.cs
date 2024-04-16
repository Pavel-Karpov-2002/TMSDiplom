using Diploma.DbStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace Diploma.DbStuff.Repositories
{
    public class FriendRepository : BaseRepository<Friend>
    {
        public FriendRepository(SocialNetworkWebDbContext context) : base(context) { }

        public void MutualAdditionToFriends(UserProfile friend, UserProfile friendTo)
        {
            var friendOne = new Friend
            {
                MainUserId = friend.UserId,
                FriendOfUser = friendTo
            };
            var friendTwo = new Friend
            {
                MainUserId = friendTo.UserId,
                FriendOfUser = friend
            };
            _entyties.Add(friendOne);
            _entyties.Add(friendTwo);
            _context.SaveChanges();
        }

        public async Task MutualAdditionToFriendsAsync(UserProfile friend, UserProfile friendTo)
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

        public void RemoveFriend(UserProfile me, UserProfile friendTo)
        {
            var friendOne = me.Friends.FirstOrDefault(f => f.MainUserId == friendTo.UserId);
            var friendTwo = friendTo.Friends.FirstOrDefault(f => f.MainUserId == me.UserId);
            _entyties.Remove(friendOne);
            _entyties.Remove(friendTwo);
            _context.SaveChangesAsync();
        }

        public List<Friend>? GetFriendsByUserId(int userId)
        {
            return _entyties
                .Include(friend => friend.FriendOfUser)
                .ThenInclude(friend => friend.User)
                .Where(friends => friends.MainUserId == userId)
                .ToList();
        }
    }
}
