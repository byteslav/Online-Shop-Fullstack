using System.Collections.Generic;
using CsharpDapperExample.Models;

namespace CsharpDapperExample.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}