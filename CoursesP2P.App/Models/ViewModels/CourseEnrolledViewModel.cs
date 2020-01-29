using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using System.Collections.Generic;

namespace CoursesP2P.App.Models.ViewModels
{
    public class CourseEnrolledViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public string Image { get; set; }

        public ICollection<CourseLecturesViewModel> Lectures { get; set; }

        public string InstructorFullName { get; set; }
    }
}
