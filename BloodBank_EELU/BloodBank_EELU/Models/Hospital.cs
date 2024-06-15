using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBank_EELU.Models
{
    public class Hospital : BaseModel
    {
        public string HospitalName { get; set; }
        public string PhoneNum { get; set; }
        public string Location { get; set; }
        public string BloodType { get; set; }
        public int NationalID { get; set; } 
    }
}
