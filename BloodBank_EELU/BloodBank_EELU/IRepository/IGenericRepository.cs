using BloodBank_EELU.Dtos;
using BloodBank_EELU.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloodBank_EELU.IRepository
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetByNameandType(string name, string type);
        Task<IEnumerable<HospitlReturnDto>> GetAllHospitals(string bloodtype);
        Task <HospitlReturnDto> GetToPlacesWithLessBlood (string bloodtype);
        Task<bool> CheackIfThisuserExistsAsync(string phoneNumber);
        Task<int> getid(string phoneNumber);
        Task<T> CreateAsync(T entity);
        Task Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
