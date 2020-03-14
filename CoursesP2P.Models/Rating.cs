using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        [Range(1, 5)]
        public int Vote { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
