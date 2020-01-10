using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.Models
{
    public class Lecture
    {
        public string Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)] 
        public string Name { get; set; }

        public string Presentation { get; set; }

        [Required]
        public string Video { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
