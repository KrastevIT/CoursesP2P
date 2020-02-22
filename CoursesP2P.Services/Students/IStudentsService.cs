using CoursesP2P.Models;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;

namespace CoursesP2P.Services.Students
{
    public interface IStudentsService
    {
        IEnumerable<CourseEnrolledViewModel> GetMyCoursesAsync(User user);

        bool Add(int courseId, string studentId);
    }
}
