using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.Models;
using CsharpDapperExample.Repository;
using CsharpDapperExample.Services.Interfaces;
using CsharpDapperExample.Utility;
using CsharpDapperExample.ViewModels;
using Microsoft.AspNetCore.Http;
using SessionExtensions = CsharpDapperExample.Utility.SessionExtensions;

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

        public async Task<HomeViewModel> GetHomeViewModelAsync()
        {
            var homeViewModel = new HomeViewModel
            {
                Products = await _productRepository.GetAllAsync(),
                Categories = await _categoryRepository.GetAllAsync()
            };
            return homeViewModel;
        }

        public async Task<DetailsViewModel> GetDetailsViewModelAsync(int id)
        {
            var shoppingCartList = SessionExtensions
                .GetItemsListFromSession<ShoppingCart>(_httpContextAccessor, WebConstants.SessionCart);
            var details = new DetailsViewModel
            {
                Product = await _productRepository.GetByIdAsync(id),
                IsExistInCart = false
            };
            if (shoppingCartList.Exists(i => i.ProductId == id))
            {
                details.IsExistInCart = true;
            }
            return details;
        }

        public void AddToCart(int id)
        {
            var shoppingCartList = SessionExtensions
                .GetItemsListFromSession<ShoppingCart>(_httpContextAccessor, WebConstants.SessionCart);
            shoppingCartList.Add(new ShoppingCart{ProductId = id});
            
            _httpContextAccessor.HttpContext?.Session.Set(WebConstants.SessionCart, shoppingCartList);
        }

        public void RemoveFromCart(int id)
        {
            var shoppingCartList = SessionExtensions
                .GetItemsListFromSession<ShoppingCart>(_httpContextAccessor, WebConstants.SessionCart);
            var itemToRemove = shoppingCartList.SingleOrDefault(c => c.ProductId == id);
            if (itemToRemove != null)
            {
                shoppingCartList.Remove(itemToRemove);
            }
            _httpContextAccessor.HttpContext?.Session.Set(WebConstants.SessionCart, shoppingCartList);
        }

        // private List<ShoppingCart> GetShoppingCartFromSession()
        // {
        //     var shoppingCart = _httpContextAccessor.HttpContext?.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
        //     if (shoppingCart != null && shoppingCart.Any())
        //     {
        //         return shoppingCart;
        //     }
        //
        //     return new List<ShoppingCart>();
        // }
    }
}