using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.Models;
using CsharpDapperExample.Repository;
using CsharpDapperExample.Services.Interfaces;
using CsharpDapperExample.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CsharpDapperExample.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        public ProductService(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products;
        }

        public async Task<ProductViewModel> GetCategoriesAsync()
        {
            var productViewModel = new ProductViewModel
            {
                CategorySelectList = await GetCategoriesListAsync()
            };
            return productViewModel;
        }
        
        public async Task<ProductViewModel> CreateProductViewModelAsync(Product product)
        {
            var productViewModel = new ProductViewModel
            {
                Product = product,
                CategorySelectList = await GetCategoriesListAsync()
            };
            return productViewModel;
        }

        public async Task CreateProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);
        }

        public async Task<ProductViewModel> GetProductViewModelByIdAsync(int id)
        {
            var productViewModel = new ProductViewModel
            {
                Product = await GetProductByIdAsync(id),
                CategorySelectList = await GetCategoriesListAsync()
            };
            return productViewModel;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }
        
        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        private async Task<IEnumerable<SelectListItem>> GetCategoriesListAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDropDown = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return categoryDropDown;
        }

    }
}