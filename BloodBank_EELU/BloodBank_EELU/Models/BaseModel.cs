using System.ComponentModel.DataAnnotations;

namespace BloodBank_EELU.Models
{
    public class BaseModel
    {
        [Key]
        public int ID { get; set; }  // Primary key
    }
}
