using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string ImageUrl { get; set; }
    }
}
