using Diploma.DbStuff.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Diploma.DbStuff.Models
{
    public class BaseModel : IBaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
