using System.Threading.Tasks;
using CsharpDapperExample.Services.Interfaces;
using CsharpDapperExample.ViewModels;
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
            var productsInCart = new CartViewModel
            {
                Products = await _cartService.GetAllProductsInCartAsync()
            };
            return View(productsInCart);
        }
        
        public IActionResult AddToCart(int id)
        {
            _cartService.AddToCart(id);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction(nameof(Index));
        }
    }
}