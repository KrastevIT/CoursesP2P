using CoursesP2P.Services.Courses;
using CoursesP2P.ViewModels.Courses.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
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
                return RedirectToAction(nameof(Create));
            }

            await this.coursesService.CreateAsync(model, this.User);

            return RedirectToAction("Index", "Instructors");
        }


        public IActionResult Details(int id)
        {
            var course = this.coursesService.Details(id);

            return View(course);
        }
    }
}