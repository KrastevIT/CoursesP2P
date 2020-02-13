using CoursesP2P.ViewModels.Courses.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Students
{
    public interface IStudentService
    {
        IEnumerable<CourseEnrolledViewModel> GetMyCourses(string studentId);

        bool Add(int courseId, string studentId);
    }
}
