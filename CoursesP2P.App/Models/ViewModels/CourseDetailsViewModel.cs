using CoursesP2P.Models.Enum;
using System.Collections.Generic;

namespace CoursesP2P.App.Models.ViewModels
{
    public class CourseDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ICollection<string> LectureName { get; set; } = new List<string>();
    }
}
