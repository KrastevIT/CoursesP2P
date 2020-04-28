using CoursesP2P.Models;
using CoursesP2P.Services.Courses;
using CoursesP2P.ViewModels.Courses.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;
        private readonly UserManager<User> userManager;

        public CoursesController(ICoursesService coursesService, UserManager<User> userManager)
        {
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        public IActionResult Category(string id)
        {
            var coursesByCategory = this.coursesService.GetCoursesByCategory(id);
            if (coursesByCategory == null)
            {
                return NotFound();
            }

            return View(coursesByCategory);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.coursesService.CreateAsync(model, user.Id, user.FirstName, user.LastName);

            return RedirectToAction("Index", "Instructors");
        }

        public IActionResult Details(int id)
        {
            var course = this.coursesService.Details(id);
            return View(course);
        }
    }
}