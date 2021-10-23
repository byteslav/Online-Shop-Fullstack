using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.Models;
using CsharpDapperExample.Repository;
using CsharpDapperExample.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CsharpDapperExample.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        public ProductController(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }
        
        public async Task<ActionResult> Create()
        {
            var productViewModel = new ProductViewModel
            {
                CategorySelectList = await GetCategoriesListAsync()
            };
            return View(productViewModel);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddAsync(product);
                return RedirectToAction("Index");
            }
            var productViewModel = new ProductViewModel
            {
                Product = product,
                CategorySelectList = await GetCategoriesListAsync()
            };
            return View(productViewModel);
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var productViewModel = new ProductViewModel
            {
                Product = product,
                CategorySelectList = await GetCategoriesListAsync()
            };
            
            return View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.UpdateAsync(product);
                return RedirectToAction("Index");
            }
            var productViewModel = new ProductViewModel
            {
                Product = product,
                CategorySelectList = await GetCategoriesListAsync()
            };
            return View(productViewModel);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            await _productRepository.DeleteAsync(id.Value);
            return RedirectToAction(nameof(Index));
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