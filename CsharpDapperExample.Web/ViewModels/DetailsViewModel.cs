using CsharpDapperExample.Entities;

namespace CsharpDapperExample.ViewModels
{
    public class DetailsViewModel
    {
        public Product Product { get; set; }
        public bool IsExistInCart { get; set; }
    }
}