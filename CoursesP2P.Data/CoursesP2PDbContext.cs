using CoursesP2P.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoursesP2P.Data
{
    public class CoursesP2PDbContext : IdentityDbContext<User>
    {
        public CoursesP2PDbContext(DbContextOptions<CoursesP2PDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Lecture> Lectures { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StudentCourse>(studentCourse =>
            {
                studentCourse.HasKey(x => new { x.StudentId, x.CourseId });

                studentCourse
                .HasOne(sc => sc.Student)
                .WithMany(s => s.EnrolledCourses)
                .HasForeignKey(sc => sc.StudentId);

                studentCourse
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(sc => sc.CourseId);

            });

            builder.Entity<Course>(course =>
            {
                course
                .HasOne(c => c.Lecturer)
                .WithMany(l => l.CreatedCourses)
                .HasForeignKey(c => c.LecturerId);
            });

            builder.Entity<Lecture>(lecture =>
            {
                lecture
                .HasOne(l => l.Course)
                .WithMany(c => c.Lectures)
                .HasForeignKey(l => l.CourseId);
            });

            base.OnModelCreating(builder);
        }
    }
}
