using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceSystem.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productId { get; set; } // system generated

        [Required]
        [MaxLength(150)]
        public string productName { get; set; } = string.Empty; // user input

        [MaxLength(1000)]
        public string? description { get; set; } // user input, optional

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(typeof(decimal), "0.01", "99999999.99")]
        public decimal price { get; set; } // user input

        [Required]
        [Range(0, int.MaxValue)]
        public int stockQuantity { get; set; } = 0; // user input, default value

        [MaxLength(300)]
        public string? imageUrl { get; set; } // user input, optional

        [Required]
        [ForeignKey("c")]
        public int categoryId { get; set; } // foreign key

        [Required]
        public DateTime createdAt { get; set; } // system generated

        public bool isAvailable { get; set; } = true; // default value

        public virtual Category c { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
