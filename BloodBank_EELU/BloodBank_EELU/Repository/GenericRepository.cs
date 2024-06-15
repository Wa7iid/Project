using BloodBank_EELU.Dtos;
using BloodBank_EELU.IRepository;
using BloodBank_EELU.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace BloodBank_EELU.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetByNameandType(string name, string type)
        {

            var hospitals = await _context.Set<Hospital>()
             .Where(h => EF.Functions.Like(h.HospitalName, name) && EF.Functions.Like(h.BloodType, type))
             .ToListAsync();
            return hospitals as IEnumerable<T>;

        }
      
        public async Task<IEnumerable<HospitlReturnDto>> GetAllHospitals(string bloodtype)
        {
            var filteredHospitals = await _context.Set<Hospital>()
                .Where(h => h.BloodType == bloodtype)
                .GroupBy(h => new { h.HospitalName, h.Location, h.BloodType })
                .Select(g => new HospitlReturnDto
                {
                    HospitalName = g.Key.HospitalName,
                    Location = g.Key.Location,
                    BloodType = g.Key.BloodType,
                    BloodTypeCount = g.Count()
                }).ToListAsync();

            return filteredHospitals;
        }

        public async Task<bool> CheackIfThisuserExistsAsync(string phoneNumber)
        {
            if (await _context.Set<AppUser>().FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber) == null){
                return true;
               }
            else{
                return false;
            }
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<int> getid(string phoneNumber)
        {
            var user = await _context.Set<AppUser>()
                           .Where(h => h.PhoneNumber == phoneNumber)
                           .Select(h => h.NtionalID)
                           .FirstOrDefaultAsync();

            return user;
        }

        public async Task<HospitlReturnDto> GetToPlacesWithLessBlood(string bloodtype)
        {
            var filteredHospitals = await _context.Set<Hospital>()
                .Where(h => h.BloodType == bloodtype )
                .GroupBy(h => new { h.HospitalName, h.Location, h.BloodType })
                 .Select(g => new HospitlReturnDto
                 {
                     HospitalName = g.Key.HospitalName,
                     Location = g.Key.Location,
                     BloodType = g.Key.BloodType,
                     BloodTypeCount = g.Count()
                 }).ToListAsync();

            int minCount = filteredHospitals.Min(h => h.BloodTypeCount);

            var hospitalsWithLessBlood = filteredHospitals
                .Where(h => h.BloodTypeCount == minCount)
                .FirstOrDefault();

            return hospitalsWithLessBlood;

        }
    }
}
