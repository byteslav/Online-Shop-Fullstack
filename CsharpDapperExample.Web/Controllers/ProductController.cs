using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.BLL.Interfaces;
using CsharpDapperExample.Entities;
using CsharpDapperExample.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CsharpDapperExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return new JsonResult(products);
        }
        
        [HttpGet("create")]
        public async Task<ActionResult> Create()
        {
            var categories = await _productService.GetCategoriesAsync();
            var productViewModel = GetProductViewModel(new Product(), categories);
            
            return new JsonResult(productViewModel);
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
            var productViewModel = GetProductViewModel(product, categories);
            
            return new JsonResult("Success!!");
        }

        [HttpGet("update")]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            var categories = await _productService.GetCategoriesAsync();
            var productViewModel = GetProductViewModel(product, categories);
            
            return new JsonResult(productViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _productService.GetCategoriesAsync();
            var productViewModel = GetProductViewModel(product, categories);
            
            //return View(productViewModel);
            return new JsonResult(productViewModel);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private ProductViewModel GetProductViewModel(Product product, IEnumerable<Category> categories)
        {
            var productViewModel = new ProductViewModel
            {
                Product = product,
                CategorySelectList = categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            return productViewModel;
        }
    }
}