using System.Collections.Generic;
using CsharpDapperExample.Entities;

namespace CsharpDapperExample.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}