using PostMicroService.DbStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace PostMicroService.DbStuff
{
    public class PostNetworkWebDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<LocationPost> LocationPosts { get; set; }

        public PostNetworkWebDbContext(DbContextOptions<PostNetworkWebDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Post>()
                .HasOne(post => post.LocationPost)
                .WithOne(location => location.Post)
                .HasForeignKey<Post>(post => post.LocationPostId);
        }
    }
}
