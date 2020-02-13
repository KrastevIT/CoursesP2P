using System.Collections.Generic;

namespace CoursesP2P.ViewModels.Courses.ViewModels
{
    public class CourseAndDashbordViewModel
    {
        public IEnumerable<CourseInstructorViewModel> Courses { get; set; }

        public int CreatedCourses { get; set; }

        public int EnrolledCourses { get; set; }

        public decimal Profit { get; set; }
    }
}
