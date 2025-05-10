using System.ComponentModel.DataAnnotations;

namespace ElectronicsEcommerce.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Label { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [MaxLength(255)]
        public string PhotoPath { get; set; }
    }
}