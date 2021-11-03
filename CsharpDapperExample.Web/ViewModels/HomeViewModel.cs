using System.Collections.Generic;
using CsharpDapperExample.Entities;

namespace CsharpDapperExample.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}