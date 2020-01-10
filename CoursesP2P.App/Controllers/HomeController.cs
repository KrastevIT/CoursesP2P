using CoursesP2P.App.Models;
using CoursesP2P.App.Models.ViewModels;
using CoursesP2P.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CoursesP2P.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly CoursesP2PDbContext coursesP2PDbContext;

        public HomeController(
            ILogger<HomeController> logger,
            CoursesP2PDbContext coursesP2PDbContext)
        {
            this.logger = logger;
            this.coursesP2PDbContext = coursesP2PDbContext;
        }

        public IActionResult Index()
        {
            var courses = this.coursesP2PDbContext.Courses.ToList();

            var models = new List<CourseViewModel>();

            foreach (var course in courses)
            {
                var model = new CourseViewModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    InstructorFullName = course.InstructorFullName,
                    Price = course.Price,
                    Category = course.Category,
                    Image = course.Image
                };

                models.Add(model);
            }

            return View(models);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
