using Diploma.DbStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace Diploma.DbStuff
{
    public class SocialNetworkWebDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Friend> Friends { get; set; }

        public SocialNetworkWebDbContext(DbContextOptions<SocialNetworkWebDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(user => user.UserProfile)
                .WithOne(userProfile => userProfile.User)
                .HasForeignKey<UserProfile>(p => p.UserId);
            builder.Entity<UserProfile>()
                .HasMany(user => user.Friends)
                .WithOne(friend => friend.FriendOfUser);
            builder.Entity<UserProfile>()
                .HasMany(user => user.Roles)
                .WithMany(roles => roles.Users);
        }
    }
}
