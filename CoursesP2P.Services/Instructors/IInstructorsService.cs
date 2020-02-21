using CoursesP2P.Models;
using CoursesP2P.ViewModels.Courses.ViewModels;

namespace CoursesP2P.Services.Instructors
{
    public interface IInstructorsService
    {
        CourseAndDashbordViewModel GetCreatedCourses(User user);

        void EditCourse(CourseEditViewModel model);
    }
}
