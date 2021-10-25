using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.Models;
using CsharpDapperExample.Repository;
using CsharpDapperExample.Services.Interfaces;
using CsharpDapperExample.Utility;
using Microsoft.AspNetCore.Http;
using SessionExtensions = CsharpDapperExample.Utility.SessionExtensions;

namespace CsharpDapperExample.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Product> _productRepository;
        public CartService(IHttpContextAccessor httpContextAccessor, IRepository<Product> productRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsInCartAsync()
        {
            var productsInCart = SessionExtensions
                .GetItemsListFromSession<ShoppingCart>(_httpContextAccessor, WebConstants.SessionCart)
                .Select(i => i.ProductId).ToList();
            var allProducts = await _productRepository.GetAllAsync();
            
            var productsList = allProducts.Where(p => productsInCart.Contains(p.Id));
            return productsList;
        }

        public void RemoveProductFromCart(int id)
        {
            var productsInCart = SessionExtensions
                .GetItemsListFromSession<ShoppingCart>(_httpContextAccessor, WebConstants.SessionCart);
            productsInCart.Remove(productsInCart.FirstOrDefault(c => c.ProductId == id));
            _httpContextAccessor.HttpContext?.Session.Set(WebConstants.SessionCart, productsInCart);
        }
    }
}