namespace Diploma.DbStuff.Models
{
    public class Friend : BaseModel
    {
        public int MainUserId { get; set; }
        public virtual UserProfile FriendOfUser { get; set; }
    }
}
