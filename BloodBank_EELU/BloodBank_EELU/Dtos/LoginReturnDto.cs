using System.ComponentModel.DataAnnotations;

namespace BloodBank_EELU.Dtos
{
    public class LoginReturnDto
    {
        public string DisplayName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public char RH { get; set; }
        [Required]
        public string BloodType { get; set; }
    }
}
