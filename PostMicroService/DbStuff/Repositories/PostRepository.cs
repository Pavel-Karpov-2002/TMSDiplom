using PostMicroService.DbStuff.Models;

namespace PostMicroService.DbStuff.Repositories
{
    public class PostRepository : BaseRepository<Post>
    {
        public PostRepository(PostNetworkWebDbContext context) : base(context)
        {
        }

        public Post GetPost(int postId)
        {
            return _entyties
                .FirstOrDefault(post => post.Id == postId);
        }

        public List<Post> GetPostsByCreator(int userId)
        {
            return _entyties
                .Where(post => post.CreatorUserId == userId)
                .ToList();
        }
    }
}
