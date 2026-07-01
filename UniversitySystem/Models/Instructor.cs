using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Models
{
    // This makes Email unique
    [Index(nameof(Email), IsUnique = true)]

        public class Instructor
        {
            // Primary Key, generated automatically
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int InstructorId { get; set; } // system generated - primary key

            // Instructor full name is required
            [Required]
            [MaxLength(100)]
            public string FullName { get; set; } = string.Empty; // user input

            // Email is required and unique
            [Required]
            [MaxLength(150)]
            public string Email { get; set; } = string.Empty; // user input - unique

            // Office number is optional
            [MaxLength(20)]
            public string? OfficeNumber { get; set; } // user input - optional

            // Hire date is required
            [Required]
            public DateTime HireDate { get; set; } // user input

            // Salary is required
            // It must be greater than 0
            [Required]
            [Column(TypeName = "decimal(18,2)")]
            [Range(typeof(decimal), "0.01", "9999999999999999.99")]
            public decimal Salary { get; set; } // user input - must be greater than 0

            // Academic title is required
            // Example: Professor, Lecturer
            [Required]
            [MaxLength(50)]
            public string AcademicTitle { get; set; } = string.Empty; // user input - from list

            // Navigation property
            // One instructor can teach many courses
            public ICollection<Course> Courses { get; set; } = new List<Course>();

            // Navigation property
            // One instructor can be head of zero or one department
            public Department? HeadedDepartment { get; set; }
        }
    
}
