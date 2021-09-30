using csharp_dapper_example.Models;
using csharp_dapper_example.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace csharp_dapper_example.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        public ProductController(IConfiguration configuration)
        {
            _productRepository = new ProductRepository(configuration);
        }

        public IActionResult Index()
        {
            var items = _productRepository.GetAll();
            return View(items);
        }
        
        public ActionResult Create()
        {
            return View();
        }
 
        [HttpPost]
        public ActionResult Create(Product product)
        {
            _productRepository.Add(product);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
        
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _productRepository.Delete(id.Value);
            return RedirectToAction("Index");
        }
    }
}