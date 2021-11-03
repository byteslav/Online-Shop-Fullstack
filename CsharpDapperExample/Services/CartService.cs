using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.Extensions;
using CsharpDapperExample.Models;
using CsharpDapperExample.Repository;
using CsharpDapperExample.Services.Interfaces;
using CsharpDapperExample.Utility;
using Microsoft.AspNetCore.Http;

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
            var productsInCart = _httpContextAccessor.HttpContext?.Session
                .GetItemsListFromSession<ShoppingCart>(WebConstants.SessionCart)
                .Select(i => i.ProductId).ToList();
            var allProducts = await _productRepository.GetAllAsync();
            
            var products = allProducts.Where(p => productsInCart.Contains(p.Id));
            return products;
        }
        
        public void AddToCart(int id)
        {
            var shoppingCartList = _httpContextAccessor.HttpContext?.Session
                .GetItemsListFromSession<ShoppingCart>(WebConstants.SessionCart);
            shoppingCartList?.Add(new ShoppingCart{ProductId = id});
            
            _httpContextAccessor.HttpContext?.Session.Set(WebConstants.SessionCart, shoppingCartList);
        }

        public void RemoveFromCart(int id)
        {
            var shoppingCartList = _httpContextAccessor.HttpContext?.Session
                .GetItemsListFromSession<ShoppingCart>(WebConstants.SessionCart);
            var itemToRemove = shoppingCartList?.SingleOrDefault(c => c.ProductId == id);
            if (itemToRemove != null)
            {
                shoppingCartList.Remove(itemToRemove);
            }
            _httpContextAccessor.HttpContext?.Session.Set(WebConstants.SessionCart, shoppingCartList);
        }
    }
}