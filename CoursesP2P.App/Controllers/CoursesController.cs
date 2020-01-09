﻿using CoursesP2P.App.Models.BindingModels;
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseBindingModel model)
        {

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images", fileName);

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
                Lecturer = user,
            };

            coursesP2PDbContext.Courses.Add(course);
            coursesP2PDbContext.SaveChanges();

            return RedirectToActionPermanentPreserveMethod("Index", "Lecturers");
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

            return RedirectToActionPermanentPreserveMethod("Index", "Lecturers");
        }

        [Authorize]
        public async Task<IActionResult> MyCourses()
        {
            var student = await this.userManager.GetUserAsync(this.User);

            var studenCourses =  this.coursesP2PDbContext.StudentCourses.Where(x => x.StudentId == student.Id).ToList();

            //foreach (var course in studenCourses)
            //{
            //    var model = new CourseViewModel()
            //    {
            //        Name = course.Name,
            //        Category = course,
            //        Image = course.Image
            //    };
            //}

            var models = new List<CourseViewModel>();

            //foreach (var course in courses)
            //{
            //    var model = new CourseViewModel()
            //    {
            //        Name = course.Name,
            //        Category = course.Category,
            //        Image = course.Image
            //    };

            //    models.Add(model);
            //}

            return View(models);
        }

        public async Task<IActionResult> Add(int id)
        {
            var course = this.coursesP2PDbContext.Courses.Find(id);
            if (course.Id == id)
            {
                return RedirectToActionPermanentPreserveMethod("Index", "Home");
            }

            //DOTO
            // var lecturer = course.Lecturer.FirstName;

            var student = await this.userManager.GetUserAsync(this.User);

            var studentCourse = new StudentCourse
            {
                Course = course,
                CourseId = course.Id,
                Student = student,
                StudentId = student.Id
            };

            course.Students.Add(studentCourse);

            student.EnrolledCourses.Add(studentCourse);

            this.coursesP2PDbContext.SaveChanges();

            return RedirectToAction("MyCourses");
        }
    }
}