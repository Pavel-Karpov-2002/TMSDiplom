using Microsoft.AspNetCore.SignalR;
using PostMicroService.ApiControllers;
using PostMicroService.DbStuff.Models;
using PostMicroService.DbStuff.Repositories;
using PostMicroService.Models;
using PostMicroService.Services;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace PostMicroService.SignalRHubs
{
    public class PostHub : Hub
    {
        private readonly PostBuilder _postBuilder;
        private readonly PostRepository _postRepository;
        private readonly LocationApi _locationApi;

        public PostHub(PostRepository postRepository, PostBuilder postBuilder, LocationApi locationApi)
        {
            _postRepository = postRepository;
            _postBuilder = postBuilder;
            _locationApi = locationApi;
        }

        public const int COUNT_LAST_POSTS = 10;

        public async Task AddNewPost(AddPostModel addPostModel)
        {
            var location = await _locationApi.GetLocationByIp(addPostModel.UserIp);
            addPostModel.Post.LocationPost = location;
            _postRepository.Add(addPostModel.Post);
            var post = addPostModel.Post;
            var sendPost = new
                {
                post.Id,
                post.CreatorUserId,
                post.CreatorAvatarUrl,
                post.CreatorUserName,
                post.DateOfCreation,
                post.Description,
                location.City,
                location.Country,
                location.CountryCode
                };
            Clients.All.SendAsync("UserGotNewPost", sendPost).Wait();
        }
        
        public void GetLastUserPosts(int userId)
        {
            var posts = _postRepository
                .GetPostsWithLocationByCreator(userId)
                .Select(post => new
                {
                    post.Id,
                    post.CreatorUserId,
                    post.CreatorAvatarUrl,
                    post.CreatorUserName,
                    post.DateOfCreation,
                    post.Description,
                    post.LocationPost.City,
                    post.LocationPost.Country,
                    post.LocationPost.CountryCode
                });
            Clients.Caller.SendAsync("LastUserPosts", posts);
        }

        public async Task<bool> DeletePost(int postId)
        {
            await _postRepository.DeleteByIdAsync(postId);
            return true;
        }

        public async Task<bool> EditPost(EditPostModel editPost)
        {
            var post = await _postRepository.GetByIdAsync(editPost.PostId);
            var updatePost = _postBuilder.RebuildEditPostToPost(post, editPost);
            _postRepository.Update(updatePost);
            return true;
        }
    }
}
