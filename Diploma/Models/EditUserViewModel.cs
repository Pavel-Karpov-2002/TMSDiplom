namespace Diploma.Models
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
