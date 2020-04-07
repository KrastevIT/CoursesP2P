using CoursesP2P.Models;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;

namespace CoursesP2P.ViewModels.Admin
{
    public class AdminUserViewModel 
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public IEnumerable<CourseViewModel> CreatedCourses { get; set; }

        public IEnumerable<AdminEnrollmentViewModel> EnrolledCourses { get; set; }

        public int Sales { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public decimal Profit { get; set; }
    }
}
