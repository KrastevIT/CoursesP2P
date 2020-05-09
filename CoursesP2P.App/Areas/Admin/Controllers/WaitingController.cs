using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursesP2P.Services.Courses;
using Microsoft.AspNetCore.Mvc;

namespace CoursesP2P.App.Areas.Admin.Controllers
{
    public class WaitingController : AdminController
    {
        private readonly ICoursesService coursesService;

        public WaitingController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public IActionResult Index()
        {
            var models = this.coursesService.GetWaitingCourses();
            return View(models);
        }
    }
}
