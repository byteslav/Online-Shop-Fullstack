using System.Threading.Tasks;
using CsharpDapperExample.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CsharpDapperExample.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var productsInCart = await _cartService.GetAllProductsInCartAsync();
            return View(productsInCart);
        }
        
        public IActionResult Remove(int id)
        {
            _cartService.RemoveProductFromCart(id);
            return RedirectToAction(nameof(Index));
        }
    }
}