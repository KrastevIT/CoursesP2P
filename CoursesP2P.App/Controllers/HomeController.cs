using CoursesP2P.Services.Courses;
using CoursesP2P.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

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
            var courses = this.coursesService.GetАpprovedCourses();
            courses = courses.OrderByDescending(x => x.Orders).Take(9);

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
