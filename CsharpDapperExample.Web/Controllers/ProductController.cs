using System.Threading.Tasks;
using CsharpDapperExample.BLL.Interfaces;
using CsharpDapperExample.Entities;
using Microsoft.AspNetCore.Mvc;

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
    }
}