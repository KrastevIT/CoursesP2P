using CoursesP2P.ViewModels.Admin;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;

namespace CoursesP2P.Services.Admin
{
    public interface IAdminService
    {
        IEnumerable<AdminUserViewModel> GetUsers();

        IEnumerable<CourseViewModel> GetCreatedCoursesByUserId(string id);

        IEnumerable<CourseViewModel> GetEnrolledCoursesByUserId(string id);
    }
}
