using CoursesP2P.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesP2P.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MinLength(22)]
        [MaxLength(500)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(1000)]
        public string Skills { get; set; }

        public int Orders { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string InstructorFullName { get; set; }

        public string InstructorId { get; set; }

        public User Instructor { get; set; }

        public Review Review { get; set; }

        public ICollection<StudentCourse> Students { get; set; } = new HashSet<StudentCourse>();

        public ICollection<Lecture> Lectures { get; set; } = new HashSet<Lecture>();

        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
    }
}
