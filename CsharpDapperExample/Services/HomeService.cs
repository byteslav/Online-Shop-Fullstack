using System.Collections.Generic;
using System.Threading.Tasks;
using CsharpDapperExample.Extensions;
using CsharpDapperExample.Models;
using CsharpDapperExample.Repository;
using CsharpDapperExample.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CsharpDapperExample.Services
{
    public class HomeService : IHomeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        
        public HomeService(IHttpContextAccessor httpContextAccessor, 
            IRepository<Product> productRepository,
            IRepository<Category> categoryRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products;
        }
        
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product;
        }

        public bool IsExistInCart(int id)
        {
            var shoppingCartList = _httpContextAccessor.HttpContext?.Session
                .GetItemsListFromSession<ShoppingCart>(WebConstants.SessionCart);
            
            return shoppingCartList != null && shoppingCartList.Exists(i => i.ProductId == id);
        }
    }
}