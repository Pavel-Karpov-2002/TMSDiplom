using Microsoft.AspNetCore.SignalR;
using PostMicroService.DbStuff.Models;
using PostMicroService.DbStuff.Repositories;
using PostMicroService.Services;

namespace CommentMoviesMicroService.SignalRHubs
{
    public class PostHub : Hub
    {
        private readonly PostBuilder _postBuilder;
        private readonly PostRepository _postRepository;

        public PostHub(PostRepository postRepository, PostBuilder postBuilder)
        {
            _postRepository = postRepository;
            _postBuilder = postBuilder;
        }

        public const int COUNT_LAST_POSTS = 10;

        public void AddNewPost(Post post)
        {
            _postRepository.Add(post);
            Clients.All.SendAsync("UserGotNewPost", post).Wait();
        }
        
        public void GetLastUserPosts(int userId)
        {
            var posts = _postRepository
                .GetPostsByCreator(userId);
            Clients.Caller.SendAsync("LastUserPosts", posts);
        }
    }
}
