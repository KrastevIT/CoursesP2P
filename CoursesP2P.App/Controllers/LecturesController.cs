using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursesP2P.App.Models.BindingModels;
using CoursesP2P.Data;
using CoursesP2P.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoursesP2P.App.Controllers
{
    public class LecturesController : Controller
    {
        private readonly CoursesP2PDbContext coursesP2PDbContext;

        public LecturesController(CoursesP2PDbContext coursesP2PDbContext)
        {
            this.coursesP2PDbContext = coursesP2PDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add(int id)
        {
            ViewBag.id = id;

            return View();
        }

        [HttpPost]
        public IActionResult Add(AddLecturesBindingModel model, int id)
        {
            //TODO
            var course = this.coursesP2PDbContext.Courses.FirstOrDefault(x => x.Id == id);

            var lecture = new Lecture
            {
                Name = model.Name,
                CourseId = id
            };

            course.Lectures.Add(lecture);
            this.coursesP2PDbContext.SaveChanges();

            return RedirectToAction("Index", "Instructors");
        }
    }
}