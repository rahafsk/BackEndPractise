using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceSystem.Models
{
    [Table("OrderItems")]
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderItemId { get; set; } // system generated

        [Required]
        [ForeignKey("Order")]
        public int orderId { get; set; } // foreign key

        [Required]
        [ForeignKey("product")]
        public int productId { get; set; } // foreign key

        [Required]
        [Range(1, 999)]
        public int quantity { get; set; } // user input

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(typeof(decimal), "0.01", "99999999.99")]
        public decimal unitPrice { get; set; } // calculated

        public virtual Order Order { get; set; } = null!;
        public virtual Product product { get; set; } = null!;
    }
}