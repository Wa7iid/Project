using System.ComponentModel.DataAnnotations;

namespace BloodBank_EELU.Dtos
{
    public class LoginDto
    {

        [Required]
        public string userName { get; set; }
        [Required]
        public string phoneNumber { get; set; }
    }
}
