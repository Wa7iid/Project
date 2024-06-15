using System.ComponentModel.DataAnnotations;

namespace BloodBank_EELU.Dtos
{
    public class ImageDto
    {
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string imageName { get; set; }
        [Required]
        public string imagePath { get; set; }
    }
}
