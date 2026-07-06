using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceSystem.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; } // system generated

        [Required]
        [MaxLength(50)]
        public string username { get; set; } = string.Empty; // user input, unique

        [Required]
        [MaxLength(150)]
        public string email { get; set; } = string.Empty; // user input, unique

        [Required]
        [MaxLength(256)]
        public string passwordHash { get; set; } = string.Empty; // user input

        [Required]
        [MaxLength(100)]
        public string fullName { get; set; } = string.Empty; // user input

        [MaxLength(20)]
        public string? phoneNumber { get; set; } // user input, optional

        [MaxLength(300)]
        public string? address { get; set; } // user input, optional

        [Required]
        public DateTime registrationDate { get; set; } // system generated

        public bool isActive { get; set; } = true; // default value

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}