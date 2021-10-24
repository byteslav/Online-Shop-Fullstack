using System.Collections.Generic;
using System.Threading.Tasks;
using CsharpDapperExample.Models;
using CsharpDapperExample.ViewModels;

namespace CsharpDapperExample.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<ProductViewModel> GetCategoriesAsync();
        Task<ProductViewModel> CreateProductViewModelAsync(Product product);
        Task CreateProductAsync(Product product);
        Task<ProductViewModel> GetProductViewModelByIdAsync(int id);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}