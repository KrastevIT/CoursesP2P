using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Students
{
    public interface IStudentsService
    {
        Task<IEnumerable<CourseEnrolledViewModel>> GetMyCoursesAsync(ClaimsPrincipal user);

        bool Add(int courseId, string studentId);
    }
}
