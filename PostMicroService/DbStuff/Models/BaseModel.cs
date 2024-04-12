using PostMicroService.DbStuff.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PostMicroService.DbStuff.Models
{
    public class BaseModel : IBaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
