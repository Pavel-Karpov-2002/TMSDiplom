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

        public async Task AddNewPost(AddPostViewModel addPostModel)
        {
            var post = _postBuilder.BuildPost(addPostModel.Post);
            var location = await _locationApi.GetLocationByIp(addPostModel.UserIp);
            post.LocationPost = location;
            _postRepository.Add(post);
            var sendPost = _postBuilder.BuildPostViewModel(post);
            Clients.All.SendAsync("UserGotNewPost", sendPost).Wait();
        }
        
        public void GetLastUserPosts(int userId)
        {
            var posts = _postRepository
                .GetPostsWithLocationByCreator(userId)
                .Select(post => _postBuilder.BuildPostViewModel(post));
            Clients.Caller.SendAsync("LastUserPosts", posts);
        }

        public async Task<bool> DeletePost(int postId)
        {
            await _postRepository.DeleteByIdAsync(postId);
            return true;
        }

        public async Task<bool> EditPost(EditPostViewModel editPost)
        {
            var post = await _postRepository.GetByIdAsync(editPost.PostId);
            var updatePost = _postBuilder.RebuildEditPostToPost(post, editPost);
            _postRepository.Update(updatePost);
            return true;
        }
    }
}
