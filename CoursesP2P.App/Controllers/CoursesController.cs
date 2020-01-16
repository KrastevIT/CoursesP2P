using CoursesP2P.App.Models.BindingModels;
using CoursesP2P.App.Models.ViewModels;
using CoursesP2P.Data;
using CoursesP2P.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CoursesP2PDbContext coursesP2PDbContext;
        private readonly UserManager<User> userManager;

        public CoursesController(CoursesP2PDbContext coursesP2PDbContext, UserManager<User> userManager)
        {
            this.coursesP2PDbContext = coursesP2PDbContext;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> MyCourses()
        {
            var student = await this.userManager.GetUserAsync(this.User);

            var courses = this.coursesP2PDbContext.StudentCourses
                .Where(sc => sc.StudentId == student.Id)
                .Select(c => c.Course)
                .ToList();

            var models = new List<CourseViewModel>();

            foreach (var course in courses)
            {
                var model = new CourseViewModel()
                {
                    Name = course.Name,
                    Category = course.Category,
                    Image = course.Image,
                    InstructorFullName = course.InstructorFullName
                };

                models.Add(model);
            }

            return View(models);
        }

        public IActionResult Category(string category)
        {
            return View();
        }


        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images", fileName);

            bool exists = System.IO.Directory.Exists("wwwroot/Images");

            if (!exists)
            {
                Directory.CreateDirectory("wwwroot/Images");
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }

            var dbPath = "/Images/" + fileName;

            var user = await this.userManager.GetUserAsync(this.User);

            var course = new Course
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Category = model.Category,
                Image = dbPath,
                InstructorFullName = user.FirstName + ' ' + user.LastName,
                InstructorId = user.Id,
            };

            coursesP2PDbContext.Courses.Add(course);
            coursesP2PDbContext.SaveChanges();

            return RedirectToAction("Index", "Instructors");
        }

        public IActionResult Edit(int id)
        {
            var course = this.coursesP2PDbContext.Courses.Find(id);

            var model = new CourseEditViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                Category = course.Category,
                Image = course.Image
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

        [Authorize]
        public async Task<IActionResult> Add(int id)
        {
            var course = this.coursesP2PDbContext.Courses.Find(id);

            var user = await this.userManager.GetUserAsync(this.User);

            var isCreatedCourseFromCurrentInstructor = course.InstructorId == user.Id;

            var existsCourse = this.coursesP2PDbContext.StudentCourses
                .Where(x => x.StudentId == user.Id)
                .ToList()
                .Any(x => x.CourseId == course.Id);

            if (existsCourse || isCreatedCourseFromCurrentInstructor)
            {
                return RedirectToAction("Index", "Home");
            }

            var studentCourse = new StudentCourse
            {
                StudentId = user.Id,
                CourseId = course.Id
            };


            this.coursesP2PDbContext.StudentCourses.Add(studentCourse);

            this.coursesP2PDbContext.SaveChanges();

            return RedirectToAction("MyCourses");
        }

        public IActionResult Details(int id)
        {
            var course = this.coursesP2PDbContext.Courses.Find(id);

            var model = new CourseDetailsViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                Category = course.Category,
                Image = course.Image
            };

            return View(model);
        }
    }
}