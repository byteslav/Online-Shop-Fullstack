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
 
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return new JsonResult("Something went wrong!");
            
            await _productService.CreateProductAsync(product);
            return new JsonResult("Successfully added!!");

        }
        
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            if (!ModelState.IsValid)
                return new JsonResult("Something went wrong!");
            
            await _productService.UpdateProductAsync(product);
            return new JsonResult("Successfully updated!");
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return new JsonResult($"Deleted by id: {id}");
        }

        [HttpGet("create")]
        public async Task<ActionResult> Create()
        {
            var categories = await _productService.GetCategoriesAsync();
            var productViewModel = GetProductViewModel(new Product(), categories);
            
            return new JsonResult(productViewModel);
        }

        [HttpGet("update")]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            var categories = await _productService.GetCategoriesAsync();
            var productViewModel = GetProductViewModel(product, categories);
            
            return new JsonResult(productViewModel);
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