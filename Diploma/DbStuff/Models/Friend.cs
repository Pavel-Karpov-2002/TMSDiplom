namespace Diploma.DbStuff.Models
{
    public class Friend : BaseModel
    {
        public int MainUserId { get; set; }
        public virtual User FriendOfUser { get; set; }
    }
}
