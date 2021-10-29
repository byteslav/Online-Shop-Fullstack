using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.Models;
using CsharpDapperExample.Services.Interfaces;
using CsharpDapperExample.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var categories = await _productService.GetCategoriesAsync();
            var productViewModel = new ProductViewModel
            {
                Product = new Product(),
                CategorySelectList = categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            return View(productViewModel);
        }
 
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _productService.GetCategoriesAsync();
            var productViewModel = new ProductViewModel
            {
                Product = product,
                CategorySelectList = categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            return View(productViewModel);
        }

        public async Task<IActionResult> Update(int id)
        {
            var categories = await _productService.GetCategoriesAsync();
            var productViewModel = new ProductViewModel
            {
                Product = await _productService.GetProductByIdAsync(id),
                CategorySelectList = categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
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

            var categories = await _productService.GetCategoriesAsync(); 
            var productViewModel = new ProductViewModel
            {
                Product = product,
                CategorySelectList = categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            return View(productViewModel);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}