using CsharpDapperExample.Models;

namespace CsharpDapperExample.ViewModels
{
    public class ProductModel
    {
        public Product Product { get; set; }
        public bool IsExistInCart { get; set; } = false;
    }
}