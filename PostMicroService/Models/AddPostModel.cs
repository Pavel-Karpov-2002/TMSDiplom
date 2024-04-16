using PostMicroService.DbStuff.Models;

namespace PostMicroService.Models
{
    public class AddPostModel
    {
        public Post Post { get; set; }
        public string UserIp { get; set; }
    }
}
