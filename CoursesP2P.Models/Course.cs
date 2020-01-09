using CoursesP2P.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public Category Category { get; set; }

        public string Image { get; set; }

        public string LecturerId { get; set; }

        public User Lecturer { get; set; }

        public ICollection<Lecture> Lectures { get; set; } = new HashSet<Lecture>();

        public ICollection<StudentCourse> Students { get; set; } = new HashSet<StudentCourse>();
    }
}
