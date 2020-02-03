using AutoMapper;
using CoursesP2P.App.Models.BindingModels.Lecture;
using CoursesP2P.App.Models.ViewModels;
using CoursesP2P.App.Models.ViewModels.Lecture;
using CoursesP2P.Data;
using CoursesP2P.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    [Authorize]
    public class LecturesController : Controller
    {
        private readonly CoursesP2PDbContext coursesP2PDbContext;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<User> userManager;

        public LecturesController(
            CoursesP2PDbContext coursesP2PDbContext,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            UserManager<User> userManager)
        {
            this.coursesP2PDbContext = coursesP2PDbContext;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            var user = this.coursesP2PDbContext.Users
                .Include(x => x.EnrolledCourses)
                .FirstOrDefault(x => x.Id == currentUser.Id);

            var isValidUser = user.EnrolledCourses.Any(x => x.CourseId == id);
            if (!isValidUser)
            {
                return Unauthorized();
            }
            var lectures = this.coursesP2PDbContext.Lectures
                .Where(x => x.CourseId == id)
                .ToList();

            var models = this.mapper.Map<IEnumerable<LectureViewModel>>(lectures);

            return View(models);
        }

        public IActionResult Add(int id)
        {
            var model = new AddLecturesBindingModel
            {
                CourseId = id
            };

            return View(model);
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1000000000)]
        [RequestSizeLimit(1000000000)]
        public IActionResult Add(AddLecturesBindingModel model)
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

            var lecture = this.mapper.Map<Lecture>(model);
            lecture.Video = dbPath;

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

            var modelVideos = this.mapper.Map<IEnumerable<VideoLectureViewModel>>(lecturesOfCourse);

            var model = this.mapper.Map<VideoViewModel>(lecture);
            model.Lectures = modelVideos;

            return View(model);
        }
    }
}