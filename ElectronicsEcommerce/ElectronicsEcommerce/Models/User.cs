using System.ComponentModel.DataAnnotations;

namespace ElectronicsEcommerce.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public string PasswordHash { get; set; }
    }
}