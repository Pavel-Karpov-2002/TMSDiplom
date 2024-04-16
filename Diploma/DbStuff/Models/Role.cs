namespace Diploma.DbStuff.Models
{
    public class Role : BaseModel
    {
        public string Name { get; set; }
        public virtual List<UserProfile>? Users { get; set; }
    }

    [Flags]
    public enum Roles
    {
        User = 1,
        Admin = 2,
        Moderator = 4
    }
}
