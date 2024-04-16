namespace PostMicroService.DbStuff.Models
{
    public class LocationPost : BaseModel
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string City { get; set; }
        public virtual Post Post { get; set; }
    }
}
