using CoursesP2P.Models;
using CoursesP2P.ViewModels.Courses.ViewModels;

namespace CoursesP2P.Services.Instructors
{
    public interface IInstructorService
    {
        CourseAndDashbordViewModel GetCreatedCourses(User instructor);

        void EditCourse(CourseEditViewModel model);
    }
}
