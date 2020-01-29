using CoursesP2P.App.Models.ViewModels.Course;
using CoursesP2P.App.Models.ViewModels.Instructor;
using CoursesP2P.Data;
using CoursesP2P.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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

            var courses = this.coursesP2PDbContext.Users
                .Where(c => c.Id == instructor.Id)
                .SelectMany(x => x.CreatedCourses).ToList();

            var models = new List<CourseInstructorViewModel>();

            foreach (var course in courses)
            {
                var orders = this.coursesP2PDbContext.StudentCourses.Where(x => x.CourseId == course.Id).ToList().Count();
                var lectures = this.coursesP2PDbContext.Lectures.Where(x => x.CourseId == course.Id).Count();

                var model = new CourseInstructorViewModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    Price = course.Price,
                    Category = course.Category,
                    Image = course.Image,
                    Orders = orders,
                    Lectures = lectures
                };

                models.Add(model);
            }

            var soldCourses = courses
                .SelectMany(x => x.Students)
                .Select(x => x.Course)
                .Count();

            var instructorDashboardViewModel = new InstructorDashboardViewModel
            {
                CreatedCourses = courses.Count,
                EnrolledCourses = soldCourses,
                Profit = instructor.Profit
            };

            var tuple = new Tuple<IEnumerable<CourseInstructorViewModel>, InstructorDashboardViewModel>(models, instructorDashboardViewModel);

            return View(tuple);
        }

        public IActionResult Edit(int id)
        {
            var course = this.coursesP2PDbContext.Courses.Find(id);
            var lecturesName = this.coursesP2PDbContext.Lectures
                .Where(x => x.CourseId == id)
                .Select(x => x.Name)
                .ToList();

            var model = new CourseEditViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                Category = course.Category,
                Image = course.Image,
                LecturesName = lecturesName
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CourseEditViewModel model)
        {
            var course = this.coursesP2PDbContext.Courses.FirstOrDefault(x => x.Id == model.Id);
            if (course == null)
            {
                return NotFound();
            }
            model.Image = course.Image;

            this.coursesP2PDbContext.Entry(course)
                 .CurrentValues.SetValues(model);

            this.coursesP2PDbContext.SaveChanges();

            return RedirectToAction("Index", "Instructors");
        }
    }
}