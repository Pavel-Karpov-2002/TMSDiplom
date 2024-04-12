namespace PostMicroService.Views
{
    public class PostViewModel
    {
        public string CreatorUserName { get; set; }
        public string CreatorAvatarUrl { get; set; }
        public string? Description { get; set; }
        public DateTime DateOfCreation { get; set; }

        public bool CanEditPost { get; set; }
        public bool CanDeletePost { get; set; }
        public bool CanAddComment { get; set; }
    }
}
