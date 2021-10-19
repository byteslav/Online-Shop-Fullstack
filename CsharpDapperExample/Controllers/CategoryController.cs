using System.Threading.Tasks;
using CsharpDapperExample.Models;
using CsharpDapperExample.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CsharpDapperExample.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryController(IRepository<Category> repository)
        {
            _categoryRepository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }
        
        public ActionResult Create()
        {
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.AddAsync(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _categoryRepository.GetByIdAsync(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.UpdateAsync(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            await _categoryRepository.DeleteAsync(id.Value);
            return RedirectToAction("Index");
        }
    }
}