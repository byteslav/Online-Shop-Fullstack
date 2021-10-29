using System.Collections.Generic;
using System.Threading.Tasks;
using CsharpDapperExample.Models;

namespace CsharpDapperExample.Services.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<Product>> GetAllProductsInCartAsync();
        void AddToCart(int id);
        void RemoveFromCart(int id);
    }
}