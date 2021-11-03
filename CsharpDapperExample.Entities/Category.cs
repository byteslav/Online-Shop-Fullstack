using System.ComponentModel.DataAnnotations;

namespace CsharpDapperExample.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}