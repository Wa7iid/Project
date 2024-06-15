using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloodBank_EELU.Models
{
    public class AppUser :BaseModel
    {
        public int NtionalID { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string BloodType { get; set; }
        public string Notes { get; set; }
    }
}
