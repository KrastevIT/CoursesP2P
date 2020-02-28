using CoursesP2P.Models;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;

namespace CoursesP2P.Services.Instructors
{
    public interface IInstructorsService
    {
        IEnumerable<CourseInstructorViewModel> GetCreatedCourses(User user);

        void EditCourse(CourseEditViewModel model);
    }
}
