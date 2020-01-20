using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoursesP2P.App.Models.BindingModels;
using CoursesP2P.Data;
using CoursesP2P.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CoursesP2P.App.Controllers
{
    public class LecturesController : Controller
    {
        private readonly CoursesP2PDbContext coursesP2PDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public LecturesController(CoursesP2PDbContext coursesP2PDbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.coursesP2PDbContext = coursesP2PDbContext;
            this.webHostEnvironment = webHostEnvironment;
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
        [RequestFormLimits(MultipartBodyLengthLimit = 1000000000)]
        [RequestSizeLimit(1000000000)]
        public IActionResult Add(AddLecturesBindingModel model, int id)
        {
            var fileName = $"{this.webHostEnvironment.WebRootPath}\\Videos\\{model.Video.FileName}";

            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                model.Video.CopyTo(fileStream);
                fileStream.Flush();
            }

            //TODO upload large Video file
            //var course = this.coursesP2PDbContext.Courses.FirstOrDefault(x => x.Id == id);

            //var lecture = new Lecture
            //{
            //    Name = model.Name,
            //    CourseId = id
            //};

            //course.Lectures.Add(lecture);
            //this.coursesP2PDbContext.SaveChanges();

            return RedirectToAction("Index", "Instructors");

        }
    }
}