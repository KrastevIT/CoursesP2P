using System.Collections.Generic;

namespace CoursesP2P.App.Models.ViewModels.Course
{
    public class CourseAndDashbordViewModel
    {
        public IEnumerable<CourseInstructorViewModel> Courses { get; set; }

        public int CreatedCourses { get; set; }

        public int EnrolledCourses { get; set; }

        public decimal Profit { get; set; }
    }
}
