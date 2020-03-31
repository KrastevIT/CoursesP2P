using CoursesP2P.Models;
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

        Task CreateAsync(CreateCourseBindingModel model, User user);

        CourseDetailsViewModel Details(int id);

        IEnumerable<CourseViewModel> Search(string searchTerm);
    }
}
