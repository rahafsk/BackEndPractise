using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Models
{
    // Department name must be unique
    [Index(nameof(DepartmentName), IsUnique = true)]

    public class Department
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; } // system generated - primary key

        // Department name is required and unique
        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty; // user input - unique

        // Building is optional
        [MaxLength(50)]
        public string? Building { get; set; } // user input - optional

        // Budget is required
        // Budget must be greater than or equal to 0
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "0.0", "9999999999999999.99")]
        public decimal Budget { get; set; } // user input

        // Foreign Key
        // Nullable because a department may temporarily have no head
        [ForeignKey(nameof(HeadInstructor))]
        public int? HeadInstructorId { get; set; } // foreign key - nullable

        // Navigation property
        // Department has one head instructor
        public Instructor? HeadInstructor { get; set; }

        // Navigation property
        // One department offers many courses
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
}
