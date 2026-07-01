using Microsoft.EntityFrameworkCore;

namespace UniversitySystem.Models
{
    public class UniversityDbContext : DbContext
    {
        // Constructor
        // DbContextOptions contains the database connection settings
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
            : base(options)
        {
        }

        // DbSet means database table
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Department - Instructor relationship
            // One department has zero or one head instructor
            // One instructor can head zero or one department
            modelBuilder.Entity<Department>()
                .HasOne(d => d.HeadInstructor)
                .WithOne(i => i.HeadedDepartment)
                .HasForeignKey<Department>(d => d.HeadInstructorId)
                .OnDelete(DeleteBehavior.SetNull);

            // Department - Course relationship
            // One department can offer many courses
            // Each course must belong to one department
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Instructor - Course relationship
            // One instructor can teach many courses
            // A course may temporarily have no instructor
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.SetNull);

            // Student - Enrollment relationship
            // One student can have many enrollment records
            // Each enrollment must belong to one student
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Course - Enrollment relationship
            // One course can have many enrollment records
            // Each enrollment must belong to one course
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Default value for GPA
            modelBuilder.Entity<Student>()
                .Property(s => s.Gpa)
                .HasDefaultValue(0.0m);

            // Default value for Enrollment Status
            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Status)
                .HasDefaultValue("In Progress");
        }
    }
}