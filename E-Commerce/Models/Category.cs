using E_Commerce.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceSystem.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryId { get; set; } // system generated

        [Required]
        [MaxLength(100)]
        public string categoryName { get; set; } = string.Empty; // user input, unique

        [MaxLength(500)]
        public string? description { get; set; } // user input, optional

        [MaxLength(300)]
        public string? imageUrl { get; set; } // user input, optional

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}