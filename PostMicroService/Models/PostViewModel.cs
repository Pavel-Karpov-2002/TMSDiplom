namespace PostMicroService.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public int CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public string? CreatorAvatarUrl { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public string? City { get; set; }
    }
}
