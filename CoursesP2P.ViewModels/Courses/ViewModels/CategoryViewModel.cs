using System.Collections.Generic;

namespace CoursesP2P.ViewModels.Courses.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}
