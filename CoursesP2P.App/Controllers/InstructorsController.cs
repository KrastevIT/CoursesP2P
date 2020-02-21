using CoursesP2P.Models;
using CoursesP2P.Services.Courses;
using CoursesP2P.Services.Instructors;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    [Authorize]
    public class InstructorsController : Controller
    {
        private readonly IInstructorsService instructorService;
        private readonly ICoursesService coursesService;
        private readonly UserManager<User> userManager;

        public InstructorsController(
            IInstructorsService instructorService,
            ICoursesService coursesService,
            UserManager<User> userManager)
        {
            this.instructorService = instructorService;
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var courses = this.instructorService.GetCreatedCourses(user);

            return View(courses);
        }

        public IActionResult Edit(int id)
        {
            var course = this.coursesService.GetCourseById(id);

            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(CourseEditViewModel model)
        {
            this.instructorService.EditCourse(model);

            return RedirectToAction("Index", "Instructors");
        }
    }
}