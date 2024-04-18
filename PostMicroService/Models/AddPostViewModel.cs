using PostMicroService.DbStuff.Models;

namespace PostMicroService.Models
{
    public class AddPostViewModel
    {
        public PostViewModel Post { get; set; }
        public string UserIp { get; set; }
    }
}
