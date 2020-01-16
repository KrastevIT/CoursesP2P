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
        public async Task<IActionResult> Index()
        {
            var instructor = await this.userManager.GetUserAsync(this.User);

            var courses = coursesP2PDbContext.Courses.Where(x => x.InstructorId == instructor.Id).ToList();

            var models = new List<CourseInstructorViewModel>();

            var enrolled = new List<StudentCourse>();

            foreach (var course in courses)
            {
                 enrolled = this.coursesP2PDbContext.StudentCourses.Where(x => x.CourseId == course.Id).ToList();

                var model = new CourseInstructorViewModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    Price = course.Price,
                    Category = course.Category,
                    Image = course.Image,
                };

                models.Add(model);
            }

            ViewBag.courses = courses.Count;
            ViewBag.enrolled = enrolled.Count;

            return View(models);
        }
    }
}