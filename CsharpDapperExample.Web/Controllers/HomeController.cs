using System.Diagnostics;
using System.Threading.Tasks;
using CsharpDapperExample.BLL.Interfaces;
using CsharpDapperExample.Entities;
using Microsoft.AspNetCore.Mvc;
using CsharpDapperExample.ViewModels;

namespace CsharpDapperExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Products = await _homeService.GetAllProductsAsync(),
                Categories = await _homeService.GetAllCategoriesAsync()
            };
            return View(homeViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var details = new DetailsViewModel
            {
                Product = await _homeService.GetProductByIdAsync(id),
                IsExistInCart = _homeService.IsExistInCart(id)
            };
            return View(details);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}