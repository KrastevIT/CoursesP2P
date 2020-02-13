using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Instructors
{
    public interface IInstructorService
    {
        Task<CourseAndDashbordViewModel> GetCreatedCourses(ClaimsPrincipal user);

        void EditCourse(CourseEditViewModel model);
    }
}
