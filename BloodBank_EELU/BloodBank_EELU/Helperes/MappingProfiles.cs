using AutoMapper;
using BloodBank_EELU.Dtos;
using BloodBank_EELU.Models;

namespace BloodBank_EELU.Helperes
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AppUserDtos,  AppUser>().ReverseMap();
            CreateMap<AppUser, LoginReturnDto>().ReverseMap();
            CreateMap<Hospital , HospitlReturnDto>().ReverseMap();
            CreateMap<Hospital , HospiatlDonation>().ReverseMap();
        }

    }
}
