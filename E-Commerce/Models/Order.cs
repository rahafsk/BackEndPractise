using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceSystem.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderId { get; set; } // system generated

        [Required]
        [ForeignKey("U")]
        public int userId { get; set; } // foreign key

        [Required]
        public DateTime orderDate { get; set; } // system generated

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(typeof(decimal), "0", "99999999.99")]
        public decimal totalAmount { get; set; } = 0; // calculated

        [Required]
        [MaxLength(30)]
        public string status { get; set; } = "Pending"; // default value

        [Required]
        [MaxLength(300)]
        public string shippingAddress { get; set; } = string.Empty; // user input

        [Required]
        [MaxLength(50)]
        public string paymentMethod { get; set; } = string.Empty; // user input

        public virtual User U { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
