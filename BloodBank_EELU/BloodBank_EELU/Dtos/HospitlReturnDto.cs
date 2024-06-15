using BloodBank_EELU.Models;
using System.ComponentModel.DataAnnotations;

namespace BloodBank_EELU.Dtos
{
    public class HospitlReturnDto
    {
        [Required]
        public string HospitalName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string BloodType { get; set; }
        public int BloodTypeCount { get; set; }
    }
}
