using System;
using System.Collections.Generic;
using System.Text;

namespace UniversitySystem.Models
{
    // Course code must be unique
    [Index(nameof(CourseCode), IsUnique = true)]
    public class Course
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; } // system generated - primary key

        // Course code is required and unique
        // Example: CS101
        [Required]
        [MaxLength(10)]
        public string CourseCode { get; set; } = string.Empty; // user input - unique

        // Course title is required
        [Required]
        [MaxLength(150)]
        public string CourseTitle { get; set; } = string.Empty; // user input

        // Credit hours must be between 1 and 6
        [Required]
        [Range(1, 6)]
        public int CreditHours { get; set; } // user input

        // Foreign Key
        // Required because each course must belong to one department
        [Required]
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; } // foreign key - not null

        // Foreign Key
        // Nullable because a course may temporarily have no instructor
        [ForeignKey(nameof(Instructor))]
        public int? InstructorId { get; set; } // foreign key - nullable

        // Semester is required
        // Example: Fall 2026
        [Required]
        [MaxLength(20)]
        public string SemesterOffered { get; set; } = string.Empty; // user input

        // Navigation property
        // Each course belongs to one department
        public Department Department { get; set; } = null!;

        // Navigation property
        // Each course may have one instructor
        public Instructor? Instructor { get; set; }

        // Navigation property
        // One course can have many enrollments
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
}
