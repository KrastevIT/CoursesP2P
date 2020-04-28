using CoursesP2P.ViewModels.Courses.BindingModels;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Courses
{
    public interface ICoursesService
    {
        IEnumerable<CourseViewModel> GetAllCourses();

        IEnumerable<CourseViewModel> GetCoursesByCategory(string id);

        Task CreateAsync(CreateCourseBindingModel model, string userId, string userFirstName, string userLastName);

        CourseDetailsViewModel Details(int id);

        IEnumerable<CourseViewModel> Search(string searchTerm);
    }
}
