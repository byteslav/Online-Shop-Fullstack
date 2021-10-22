using System.Collections.Generic;
using CsharpDapperExample.Models;

namespace CsharpDapperExample.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}