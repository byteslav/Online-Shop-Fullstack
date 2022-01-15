using System.Threading.Tasks;
using CsharpDapperExample.BLL.Interfaces;
using CsharpDapperExample.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CsharpDapperExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return new JsonResult(categories);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
                return new JsonResult("Something went wrong!");
            
            await _categoryService.CreateCategoryAsync(category);
            return new JsonResult("Successfully added!");
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(Category category)
        {
            if (!ModelState.IsValid)
                return new JsonResult("Something went wrong!");
            
            await _categoryService.UpdateCategoryAsync(category);
            return new JsonResult("Successfully updated!");
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return new JsonResult($"Deleted by id: {id}");
        }
    }
}