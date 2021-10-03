using System.ComponentModel.DataAnnotations;

namespace csharp_dapper_example.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Count { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public float Price { get; set; }
    }
}