using AutoMapper;
using BloodBank_EELU.Dtos;
using BloodBank_EELU.IRepository;
using BloodBank_EELU.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BloodBank_EELU.Controllers
{
    public class HospitalController : BaseController
    {
        private readonly IGenericRepository<Hospital> _hospitalRepository;
        private readonly IMapper _mapper;

        public HospitalController(IGenericRepository<Hospital> genericRepository , IMapper mapper)
        {
            _hospitalRepository = genericRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllHospitals")]
        public async Task<ActionResult<IEnumerable<ApiResponse<HospitlReturnDto>>>> GetAllHospitals(string bloodtype)
        {
            var hospitals = await _hospitalRepository.GetAllHospitals(bloodtype);
        
            return Ok(hospitals);
        }

        [HttpPost("Donate")]
        public async Task<ActionResult<Hospital>> Donation(HospiatlDonation hospitl)
        {
            var Map = _mapper.Map<HospiatlDonation, Hospital>(hospitl);

            if (ModelState.IsValid)
            {
                var Data = new Hospital()
                {
                    HospitalName = hospitl.HospitalName,
                    PhoneNum = hospitl.PhoneNum,
                    Location = hospitl.Location,
                    BloodType = hospitl.BloodType,
                    NationalID = hospitl.NationalID,
                };
                await _hospitalRepository.CreateAsync(Data);
            }
            return Ok();
        }

        [HttpGet("GetToPlacesWithLessBlood")]
        public async Task<ActionResult> GetToPlacesWithLessBlood(string bloodtype)
        {
            var place = await _hospitalRepository.GetToPlacesWithLessBlood(bloodtype);

            return Ok(place);
        }

    }
}
