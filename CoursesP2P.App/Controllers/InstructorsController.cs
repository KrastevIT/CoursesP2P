using CoursesP2P.Services.Courses;
using CoursesP2P.Services.Instructors;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    [Authorize]
    public class InstructorsController : Controller
    {
        private readonly IInstructorService instructorService;
        private readonly ICoursesService coursesService;

        public InstructorsController(
            IInstructorService instructorService,
            ICoursesService coursesService)
        {
            this.instructorService = instructorService;
            this.coursesService = coursesService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await this.instructorService.GetCreatedCourses(this.User);

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