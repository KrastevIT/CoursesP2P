using CoursesP2P.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(22)]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0, 5000.00, ErrorMessage = "Invalid Target Price; Max 5000.00")]
        public decimal Price { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string InstructorFullName { get; set; }

        public string InstructorId { get; set; }

        public User Instructor { get; set; }

        public ICollection<StudentCourse> Students { get; set; } = new HashSet<StudentCourse>();

        public ICollection<Lecture> Lectures { get; set; } = new HashSet<Lecture>();
    }
}
