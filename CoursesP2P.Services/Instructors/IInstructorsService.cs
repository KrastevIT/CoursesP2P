using CoursesP2P.Models;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;

namespace CoursesP2P.Services.Instructors
{
    public interface IInstructorsService
    {
        IEnumerable<CourseInstructorViewModel> GetCreatedCourses(User user);

        CourseEditViewModel GetCourseById(int id, string userId);

        void EditCourse(CourseEditViewModel model);
    }
}
