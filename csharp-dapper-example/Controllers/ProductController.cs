using System.Threading.Tasks;
using csharp_dapper_example.Models;
using csharp_dapper_example.Repository;
using Microsoft.AspNetCore.Mvc;

namespace csharp_dapper_example.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        public ProductController(IRepository<Product> repository)
        {
            _productRepository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _productRepository.GetAllAsync();
            return View(items);
        }
        
        public ActionResult Create()
        {
            return View();
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

            return View(product);
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.UpdateAsync(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            await _productRepository.DeleteAsync(id.Value);
            return RedirectToAction("Index");
        }
    }
}