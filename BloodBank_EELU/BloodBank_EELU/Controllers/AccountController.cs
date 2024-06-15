using AutoMapper;
using BloodBank_EELU.Dtos;
using BloodBank_EELU.IRepository;
using BloodBank_EELU.Models;
using BloodBank_EELU.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BloodBank_EELU.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IGenericRepository<AppUser> _appuserRepository;
        private readonly IMapper _mapper;
        private readonly StoreContext _context;
        private readonly ISMSServices _services;

        public AccountController(IGenericRepository<AppUser> AppuserRepository, IMapper mapper, StoreContext context , ISMSServices services)
        {
            _appuserRepository = AppuserRepository;
            _mapper = mapper;
            _context = context;
            _services = services;
        }


        [HttpPost("Register")]
        public async Task<ActionResult> CreateAccount(AppUserDtos appUser)
        {
            var Cheack = await _appuserRepository.CheackIfThisuserExistsAsync(appUser.userName);

            if (Cheack == false){ return BadRequest(new { message = "This User Already Exists" }); }

            var Mapper = _mapper.Map<AppUserDtos, AppUser>(appUser);

            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    Username = Mapper.Username,
                    Address = Mapper.Address,
                    PhoneNumber = Mapper.PhoneNumber,
                    BloodType = Mapper.BloodType,
                };
                await _appuserRepository.CreateAsync(user);
            };

            int  id = await _appuserRepository.getid(appUser.phoneNumber);

            string phone = appUser.phoneNumber.Substring(1);

            string massageSMS = $"Welcome {appUser.userName} to our blood donation service," +
                $"Please Save Your Id : {id} " +
                                                           $"Log in to search for blood bags or donate " +
                                                           $"Thanks for joining us";

            string phoneSMS = $"+20{phone}";

            //var sms = _services.Send(phoneSMS,massageSMS);

            //if(!string.IsNullOrEmpty(sms.ErrorMessage))
            //{
            //   return BadRequest(new { message = sms.ErrorMessage });
            //}


            return Ok(new {message = "User created successfully" });
        }

        [HttpPost("sendsms")]
        public  IActionResult Send(SendSMSDto dto)
        {
            var result = _services.Send(dto.MobileNumber, dto.Body);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserDtos>> login(LoginDto loginDto)
        {
            var Cheack = await _appuserRepository.CheackIfThisuserExistsAsync(loginDto.phoneNumber);

            if (Cheack == true) return Unauthorized(401);

            var user = await _context.Set<AppUser>().FirstOrDefaultAsync(P => P.PhoneNumber == loginDto.phoneNumber);

            return Ok(_mapper.Map<AppUser, LoginReturnDto>(user));
        }

        [HttpDelete("{phoneNumber}")]
        public async Task<ActionResult> DeletAccount(string phoneNumber)
        {
            var user = await _context.Set<AppUser>().FirstOrDefaultAsync(P => P.PhoneNumber == phoneNumber);

            if (user != null)
            {
                _appuserRepository.Delete(user);

                await _context.SaveChangesAsync();

                return Ok("User deleted successfully.");

            }
            else
            {
                return NotFound("User not found.");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> UpdateData(AppUser user)
        {
            var existingUser = await _context.Set<AppUser>().FirstOrDefaultAsync(p => p.PhoneNumber == user.PhoneNumber);

            if (existingUser != null)
            {
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Address = user.Address;
                existingUser.Username = user.Username;
                existingUser.BloodType = user.BloodType;
                existingUser.Notes = user.Notes;

                await _appuserRepository.Update(existingUser);

                return Ok();
            }
            else
            {
                return NotFound("Add Useful Data");
            }
        }


    }
}
    

