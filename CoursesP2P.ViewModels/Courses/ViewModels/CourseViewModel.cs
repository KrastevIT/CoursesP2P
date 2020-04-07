using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Lectures.ViewModels;
using System.Collections.Generic;

namespace CoursesP2P.ViewModels.Courses.ViewModels
{
    public class CourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName =>
           this.Name?.Length > 29
           ? this.Name.Substring(0, 29) + "..."
           : this.Name;

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public string Image { get; set; }

        public string InstructorFullName { get; set; }

        public int Orders { get; set; }

        public ICollection<CourseLecturesViewModel> Lectures { get; set; }
    }
}
