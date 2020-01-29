using CoursesP2P.Models.Enum;
using System.Collections.Generic;

namespace CoursesP2P.App.Models.ViewModels.Course
{
    public class CourseEditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public string Image { get; set; }

        public ICollection<string> LecturesName { get; set; } = new List<string>();
    }
}
