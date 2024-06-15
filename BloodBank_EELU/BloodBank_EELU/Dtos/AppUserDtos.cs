using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBank_EELU.Dtos
{
    public class AppUserDtos
    {
        [Required]
        public string userName { get; set; }
        public string phoneNumber { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string bloodType { get; set; }

    }
}
