using System.Threading.Tasks;
using CsharpDapperExample.Models;
using CsharpDapperExample.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CsharpDapperExample.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }
        
        public async Task<ActionResult> Create()
        {
            var productViewModel = await _productService.GetCategoriesAsync();
            return View(productViewModel);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var productViewModel = await _productService.CreateProductViewModelAsync(product);
            return View(productViewModel);
        }

        public async Task<IActionResult> Update(int id)
        {
            var productViewModel = await _productService.GetProductViewModelByIdAsync(id);
            return View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var productViewModel = await _productService.CreateProductViewModelAsync(product);
            return View(productViewModel);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}