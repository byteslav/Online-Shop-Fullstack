using System.Threading.Tasks;
using CsharpDapperExample.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            return Ok();
        }

        public async Task<IActionResult> Details(int id)
        {
            return Ok();
        }
    }
}