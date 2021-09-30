using System.ComponentModel.DataAnnotations;

namespace csharp_dapper_example.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }
    }
}