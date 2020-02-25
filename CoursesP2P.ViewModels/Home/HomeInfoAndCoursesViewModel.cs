using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;

namespace CoursesP2P.ViewModels.Home
{
    public class HomeInfoAndCoursesViewModel
    {
        public IEnumerable<CourseViewModel> AllCourses { get; set; }

        public int Students { get; set; }

        public int Instructors { get; set; }

        public int Courses { get; set; }

        public int Lectures { get; set; }
    }
}
