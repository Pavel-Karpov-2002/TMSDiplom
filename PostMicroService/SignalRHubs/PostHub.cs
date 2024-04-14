using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using PostMicroService.DbStuff.Models;
using PostMicroService.DbStuff.Repositories;
using PostMicroService.Models;
using PostMicroService.Services;

namespace PostMicroService.SignalRHubs
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

        public async Task<bool> DeletePost(int postId)
        {
            await _postRepository.DeleteByIdAsync(postId);
            return true;
        }

        public async Task<bool> EditPost(EditPostView editPost)
        {
            var post = await _postRepository.GetByIdAsync(editPost.PostId);
            var updatePost = _postBuilder.RebuildEditPostViewToPost(post, editPost);
            _postRepository.Update(updatePost);
            return true;
        }
    }
}
