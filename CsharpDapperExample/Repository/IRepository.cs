using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsharpDapperExample.Repository
{
    public interface IRepository<T> where T: class
    {
        Task AddAsync(T item);
        Task DeleteAsync(int id);
        Task UpdateAsync(T item);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}