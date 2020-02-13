using CoursesP2P.Services.Courses;
using CoursesP2P.ViewModels.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoursesP2P.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICoursesService coursesService;

        public HomeController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public IActionResult Index()
        {
            var courses = this.coursesService.GetAllCourses();

            return View(courses);
        }

        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction("Index");
            }

            var foundCourses = this.coursesService.Search(searchTerm);

            return View(foundCourses);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
