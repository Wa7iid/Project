using BloodBank_EELU.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBank_EELU.Dtos
{
    public class HospiatlDonation
    {
        public string HospitalName { get; set; }
        public string PhoneNum { get; set; }
        public string Location { get; set; }
        public string BloodType { get; set; }
        public int NationalID { get; set; }
    }
}
