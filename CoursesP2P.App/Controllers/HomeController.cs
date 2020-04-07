using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Courses;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace CoursesP2P.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICoursesService coursesService;
        private readonly CoursesP2PDbContext db;

        public HomeController(ICoursesService coursesService, CoursesP2PDbContext db)
        {
            this.coursesService = coursesService;
            this.db = db;
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

        public IActionResult Test()
        {
            var test = this.db.Courses.To<CourseViewModel>().ToList();

            return View();
        }
    }
}
