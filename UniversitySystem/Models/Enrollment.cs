using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UniversitySystem.Models
{
    public class Enrollment
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentId { get; set; } // system generated - primary key

        // Foreign Key
        // Required because enrollment must belong to one student
        [Required]
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; } // foreign key - not null

        // Foreign Key
        // Required because enrollment must belong to one course
        [Required]
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; } // foreign key - not null

        // Enrollment date is required
        [Required]
        public DateTime EnrollmentDate { get; set; } // user input or system generated

        // Final grade is optional until the course is graded
        // Example: A, B+
        [MaxLength(2)]
        public string? FinalGrade { get; set; } // user input - optional

        // Status is required
        // Default value is In Progress
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "In Progress"; // default value

        // Navigation property
        // Each enrollment belongs to one student
        public Student Student { get; set; } = null!;

        // Navigation property
        // Each enrollment belongs to one course
        public Course Course { get; set; } = null!;
    }
}

