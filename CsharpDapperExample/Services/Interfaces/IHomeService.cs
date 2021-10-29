using System.Collections.Generic;
using System.Threading.Tasks;
using CsharpDapperExample.Models;
using CsharpDapperExample.ViewModels;

namespace CsharpDapperExample.Services.Interfaces
{
    public interface IHomeService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Product> GetProductByIdAsync(int id);
        bool IsExistInCart(int id);
    }
}