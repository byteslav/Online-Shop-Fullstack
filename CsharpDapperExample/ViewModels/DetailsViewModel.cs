using CsharpDapperExample.Models;

namespace CsharpDapperExample.ViewModels
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {
            Product = new Product();
        }
        public Product Product { get; set; }
        public bool IsExistInCart { get; set; } = false;
    }
}