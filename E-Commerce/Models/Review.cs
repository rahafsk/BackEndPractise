using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceSystem.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reviewId { get; set; } // system generated

        [Required]
        [ForeignKey("U")]
        public int userId { get; set; } // foreign key

        [Required]
        [ForeignKey("product")]
        public int productId { get; set; } // foreign key

        [Required]
        [Range(1, 5)]
        public int rating { get; set; } // user input

        [MaxLength(1000)]
        public string? comment { get; set; } // user input, optional

        [Required]
        public DateTime reviewDate { get; set; } // system generated

        public virtual User U { get; set; } = null!;
        public virtual Product product { get; set; } = null!;
    }
}