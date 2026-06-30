using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UniversitySystem.Models
{
    internal class Students
    {
        // This index makes Email unique in the database
        [Index(nameof(Email), IsUnique = true)]
        public class Student
        {
            // Primary Key
            // DatabaseGenerated means the database will generate the ID automatically
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int StudentId { get; set; } // system generated - primary key

            // Required means the student name cannot be empty
            // MaxLength means the name cannot be more than 100 characters
            [Required]
            [MaxLength(100)]
            public string FullName { get; set; } = string.Empty; // user input

            // Email is required, unique, and maximum 150 characters
            [Required]
            [MaxLength(150)]
            public string Email { get; set; } = string.Empty; // user input - unique

            // Phone number is optional because it has ?
            // Max length is 20 characters
            [MaxLength(20)]
            public string? PhoneNumber { get; set; } // user input - optional

            // Date of birth is required
            [Required]
            public DateTime DateOfBirth { get; set; } // user input

            // Enrollment year must be between 2000 and 2030
            [Required]
            [Range(2000, 2030)]
            public int EnrollmentYear { get; set; } // user input

            // GPA is decimal
            // Default value is 0.0
            // Range must be between 0.0 and 4.0
            [Column(TypeName = "decimal(3,2)")]
            [Range(typeof(decimal), "0.0", "4.0")]
            public decimal Gpa { get; set; } = 0.0m; // default value

            // Navigation property
            // One student can have many enrollments
            public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        }
    }
}
