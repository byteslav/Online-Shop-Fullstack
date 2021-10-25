using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CsharpDapperExample.Models;
using CsharpDapperExample.Services.Interfaces;

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
            var homeViewModel = await _homeService.GetHomeViewModelAsync();
            return View(homeViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var details = await _homeService.GetDetailsViewModelAsync(id);
            return View(details);
        }

        public IActionResult AddToCart(int id)
        {
            _homeService.AddToCart(id);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult RemoveFromCart(int id)
        {
            _homeService.RemoveFromCart(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}