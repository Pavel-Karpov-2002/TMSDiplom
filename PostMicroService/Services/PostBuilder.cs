using PostMicroService.DbStuff.Models;
using PostMicroService.Models;
using PostMicroService.Services.Interfaces;

namespace PostMicroService.Services
{
    public class PostBuilder : IService
    {
        public Post RebuildEditPostToPost(Post post, EditPostViewModel editPost)
        {
            post.Description = editPost.Description;
            return post;
        }

        public Post BuildPost(PostViewModel post)
        {
            return new Post
            {
                DateOfCreation = post.DateOfCreation,
                Description = post.Description,
                CreatorAvatarUrl = post.CreatorAvatarUrl,
                CreatorUserId = post.CreatorUserId,
                CreatorUserName = post.CreatorUserName,
                LocationPost = new LocationPost
                {
                    City = post.City,
                    Country = post.Country,
                    CountryCode = post.CountryCode
                }
            };
        }

        public PostViewModel BuildPostViewModel(Post post)
        {
            return new PostViewModel
            {
                Id = post.Id,
                DateOfCreation = post.DateOfCreation,
                Description = post.Description,
                CreatorAvatarUrl = post.CreatorAvatarUrl,
                CreatorUserId = post.CreatorUserId,
                CreatorUserName = post.CreatorUserName,
                City = post.LocationPost.City,
                Country = post.LocationPost.Country,
                CountryCode = post.LocationPost.CountryCode
            };
        }
    }
}
