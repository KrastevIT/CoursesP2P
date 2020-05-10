using CoursesP2P.ViewModels.Courses.BindingModels;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Courses
{
    public interface ICoursesService
    {
        IEnumerable<CourseViewModel> GetАpprovedCourses();

        IEnumerable<CourseViewModel> GetWaitingCourses();

        IEnumerable<CourseViewModel> GetCoursesByCategory(string id);

        Task CreateAsync(CreateCourseBindingModel model, string userId, string userFirstName, string userLastName, string imageUrl);

        CourseDetailsViewModel Details(int id);

        IEnumerable<CourseViewModel> Search(string searchTerm);
    }
}
