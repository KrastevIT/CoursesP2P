using System.Collections.Generic;

namespace CoursesP2P.ViewModels.Courses.ViewModels
{
    public class CourseDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ICollection<string> Skills { get; set; } = new List<string>();

        public ICollection<string> LectureName { get; set; } = new List<string>();
    }
}
