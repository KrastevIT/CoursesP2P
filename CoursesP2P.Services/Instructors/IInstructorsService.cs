using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;

namespace CoursesP2P.Services.Instructors
{
    public interface IInstructorsService
    {
        IEnumerable<CourseInstructorViewModel> GetCreatedCourses(string userId);

        CourseEditViewModel GetCourseById(int id, string userId);

        void EditCourse(CourseEditViewModel model);
    }
}
