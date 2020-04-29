using CoursesP2P.Models;
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
        private readonly UserManager<User> userManager;

        public InstructorsController(
            IInstructorsService instructorService,
            UserManager<User> userManager)
        {
            this.instructorService = instructorService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);
            var courses = this.instructorService.GetCreatedCourses(userId);

            return View(courses);
        }

        public IActionResult Edit(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var course = this.instructorService.GetCourseById(id, userId);

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseEditViewModel model)
        {
            await this.instructorService.EditCourseAsync(model);

            return RedirectToAction("Index", "Instructors");
        }
    }
}