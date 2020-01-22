using CoursesP2P.App.Models.BindingModels;
using CoursesP2P.App.Models.ViewModels;
using CoursesP2P.Data;
using CoursesP2P.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public IActionResult Index(int id)
        {
            var lectures = this.coursesP2PDbContext.Lectures
                .Where(x => x.CourseId == id)
                .ToList();

            var models = new List<LectureViewModel>();

            foreach (var lecture in lectures)
            {
                var model = new LectureViewModel
                {
                    Id = lecture.Id,
                    Name = lecture.Name,
                    Video = lecture.Video
                };

                models.Add(model);
            }

            return View(models);
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
            var guidName = Guid.NewGuid().ToString() + Path.GetExtension(model.Video.FileName);

            var filePath = $"{this.webHostEnvironment.WebRootPath}\\Videos\\{guidName}";

            bool exists = System.IO.Directory.Exists("wwwroot/Videos");
            if (!exists)
            {
                Directory.CreateDirectory("wwwroot/Videos");
            }

            using (FileStream fileStream = System.IO.File.Create(filePath))
            {
                model.Video.CopyTo(fileStream);
                fileStream.Flush();
            }

            var dbPath = "/Videos/" + guidName;

            var lecture = new Lecture
            {
                Name = model.Name,
                CourseId = id,
                Video = dbPath
            };

            this.coursesP2PDbContext.Lectures.Add(lecture);
            this.coursesP2PDbContext.SaveChanges();

            return RedirectToAction("Index", "Instructors");
        }

        public IActionResult Video(int id)
        {
            var lecture = this.coursesP2PDbContext.Lectures.FirstOrDefault(x => x.Id == id);

            var lecturesOfCourse = this.coursesP2PDbContext.Lectures
                .Where(x => x.CourseId == lecture.CourseId)
                .ToList();

            var modelVideos = new List<VideoLectureViewModel>();

            foreach (var video in lecturesOfCourse)
            {

                var modelVideoLecture = new VideoLectureViewModel
                {
                    Id = video.Id,
                    Name = video.Name,
                    VideoPath = video.Video
                };

                modelVideos.Add(modelVideoLecture);
            }

            var model = new VideoViewModel

            {
                Name = lecture.Name,
                VideoPath = lecture.Video,
                Lectures = modelVideos
            };

            return View(model);
        }
    }
}