using AutoMapper;
using CoursesP2P.App.Models;
using CoursesP2P.App.Models.ViewModels.Course;
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
        private readonly IMapper mapper;

        public HomeController(
            ILogger<HomeController> logger,
            CoursesP2PDbContext coursesP2PDbContext,
            IMapper mapper)
        {
            this.logger = logger;
            this.coursesP2PDbContext = coursesP2PDbContext;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var courses = this.coursesP2PDbContext.Courses.ToList();

            var models = new List<CourseViewModel>();

            foreach (var course in courses)
            {
                if (course.Name.Length >= 43)
                {
                    course.Name = course.Name.Substring(0, 43);
                }

                var enrolled = this.coursesP2PDbContext.StudentCourses.Where(x => x.CourseId == course.Id).ToList().Count();
                var lectures = this.coursesP2PDbContext.Lectures.Where(x => x.CourseId == course.Id).Count();

                var model = new CourseViewModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    InstructorFullName = course.InstructorFullName,
                    Price = course.Price,
                    Category = course.Category,
                    Image = course.Image,
                    Orders = enrolled,
                    Lectures = lectures
                };

                models.Add(model);
            }

            return View(models);
        }

        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction("Index");
            }

            var searchResult = new List<CourseViewModel>();

            var foundCourses = this.coursesP2PDbContext
                .Courses
                .Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()))
                .Select(course => new CourseViewModel()
                {
                    Id = course.Id,
                    Name = course.Name,
                    InstructorFullName = course.InstructorFullName,
                    Price = course.Price,
                    Category = course.Category,
                    Image = course.Image
                })
                .ToList();

            searchResult.AddRange(foundCourses);

            return View(searchResult);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
