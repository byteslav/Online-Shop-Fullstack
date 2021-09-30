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
    }
}