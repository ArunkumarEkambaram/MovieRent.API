using MovieRent.API.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRent.API.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
         Task<IList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<bool> CreateAsync(T entity);

        Task<bool> UpdateAsync(int id, T entity);

        Task<bool> DeleteAsync(int id); 
    }
}
