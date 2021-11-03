using System.Collections.Generic;
using CsharpDapperExample.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CsharpDapperExample.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
    }
}