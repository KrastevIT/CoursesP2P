using AutoMapper;
using CoursesP2P.App.Models.ViewModels.Course;
using CoursesP2P.App.Models.ViewModels.Instructor;
using CoursesP2P.Data;
using CoursesP2P.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly CoursesP2PDbContext coursesP2PDbContext;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public InstructorsController(
            CoursesP2PDbContext coursesP2PDbContext,
            IMapper mapper,
            UserManager<User> userManager)
        {
            this.coursesP2PDbContext = coursesP2PDbContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var instructor = await this.userManager.GetUserAsync(this.User);

            var courses = this.coursesP2PDbContext.Courses
                .Where(x => x.InstructorId == instructor.Id)
                .Include(x => x.Lectures)
                .Include(x => x.Students)
                .ToList();

            var models = new List<CourseInstructorViewModel>();

            courses.ForEach(course => models.Add(this.mapper.Map<CourseInstructorViewModel>(course)));

            var dashbordItems = new InstructorDashboardViewModel
            {
                CreatedCourses = courses.Count(),
                EnrolledCourses = courses.Select(x => x.Students).Sum(x => x.Count),
                Profit = instructor.Profit
            };

            this.ViewBag.dashbordItems = dashbordItems;

            return View(models);
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