using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Instructors
{
    public interface IInstructorsService
    {
        IEnumerable<CourseInstructorViewModel> GetCreatedCourses(string userId);

        CourseEditViewModel GetCourseById(int id, string userId);

        Task EditCourseAsync(CourseEditViewModel model, string userId);

        void Active(int courseId);
    }
}
