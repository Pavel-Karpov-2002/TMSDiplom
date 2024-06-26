﻿namespace Diploma.DbStuff.Models
{
    public class UserProfile: BaseModel
    {
        public DateTime? Birthday { get; set; }
        public string? PreferLocale { get; set; }

        public virtual List<Role>? Roles { get; set; }
        public virtual List<Friend>? Friends { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
