using PostMicroService.DbStuff.Models;
using PostMicroService.Models;
using PostMicroService.Services.Interfaces;
using PostMicroService.Views;


namespace PostMicroService.Services
{
    public class PostBuilder : IService
    {
        public Post BuildPost(DateTime dateOfCreation, string description, int creatorUserId)
        {
            return new Post
            {
                DateOfCreation = dateOfCreation,
                Description = description,
                CreatorUserId = creatorUserId
            };
        }

        public PostViewModel BuildPostViewModel(Post post)
        {
            return new PostViewModel
            {
                CreatorUserName = post.CreatorUserName,
                DateOfCreation = post.DateOfCreation.UtcDateTime,
                CreatorAvatarUrl = post.CreatorAvatarUrl,
                Description = post.Description
            };
        }

        public Post RebuildEditPostViewToPost(Post post, EditPostView editPost)
        {
            post.Description = editPost.Description;
            return post;
        }
    }
}
