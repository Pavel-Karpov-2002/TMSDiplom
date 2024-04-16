using PostMicroService.DbStuff.Models;
using PostMicroService.Models;
using PostMicroService.Services.Interfaces;

namespace PostMicroService.Services
{
    public class PostBuilder : IService
    {
        public Post RebuildEditPostToPost(Post post, EditPostModel editPost)
        {
            post.Description = editPost.Description;
            return post;
        }
    }
}
