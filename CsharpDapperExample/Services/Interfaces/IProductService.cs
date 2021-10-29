using System.Collections.Generic;
using System.Threading.Tasks;
using CsharpDapperExample.Models;

namespace CsharpDapperExample.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}