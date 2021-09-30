using System.Collections.Generic;
using System.Threading.Tasks;
using csharp_dapper_example.Models;

namespace csharp_dapper_example.Repository
{
    public interface IRepository<T>
    {
        Task AddAsync(T item);
        Task DeleteAsync(int id);
        Task UpdateAsync(T item);
        Task<Product> GetByIdAsync(int? id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}