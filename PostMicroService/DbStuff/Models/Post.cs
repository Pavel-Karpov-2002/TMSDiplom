namespace PostMicroService.DbStuff.Models
{
    public class Post : BaseModel
    {
        public virtual int CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        public string? CreatorAvatarUrl { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
