using System.Collections.Generic;
using System.Threading.Tasks;
using CsharpDapperExample.Entities;

namespace CsharpDapperExample.BLL.Interfaces
{
    public interface IHomeService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Product> GetProductByIdAsync(int id);
        bool IsExistInCart(int id);
    }
}