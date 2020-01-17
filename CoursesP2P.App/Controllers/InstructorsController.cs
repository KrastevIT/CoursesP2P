using CoursesP2P.App.Models.ViewModels;
using CoursesP2P.Data;
using CoursesP2P.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly CoursesP2PDbContext coursesP2PDbContext;
        private readonly UserManager<User> userManager;

        public InstructorsController(CoursesP2PDbContext coursesP2PDbContext, UserManager<User> userManager)
        {
            this.coursesP2PDbContext = coursesP2PDbContext;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var instructorId = this.userManager.GetUserId(this.User);

            var courses = this.coursesP2PDbContext.Users
                .Where(c => c.Id == instructorId)
                .SelectMany(x => x.CreatedCourses).ToList();

            var models = new List<CourseInstructorViewModel>();

            foreach (var course in courses)
            {
                var orders = this.coursesP2PDbContext.StudentCourses.Where(x => x.CourseId == course.Id).ToList().Count();

                var model = new CourseInstructorViewModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    Price = course.Price,
                    Category = course.Category,
                    Image = course.Image,
                    Orders = orders
                };

                models.Add(model);
            }

            var enrolledCourses = courses.SelectMany(x => x.Students).Select(x => x.Course).ToList();
            var totalProfit = enrolledCourses.Select(x => x.Price).Sum();

            ViewBag.courses = courses.Count;
            ViewBag.enrolled = enrolledCourses.Count;
            ViewBag.profit = totalProfit;

            return View(models);
        }
    }
}