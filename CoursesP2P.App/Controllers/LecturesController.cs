using CoursesP2P.Models;
using CoursesP2P.Services.Lectures;
using CoursesP2P.ViewModels.Lectures.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    [Authorize]
    public class LecturesController : Controller
    {
        private readonly ILecturesService lectureService;
        private readonly UserManager<User> userManager;

        public LecturesController(
            ILecturesService lectureService,
            UserManager<User> userManager)
        {
            this.lectureService = lectureService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var lectures = this.lectureService.GetLecturesByCourseIdAsync(id, user);

            return View(lectures);
        }

        public async Task<IActionResult> Add(int id)
        {
            var instructor = await this.userManager.GetUserAsync(this.User);

            var model = this.lectureService.GetLectureBindingModelWithCourseId(id, instructor);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddLecturesBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.lectureService.Add(model);

            return RedirectToAction("Index", "Instructors");
        }

        public IActionResult Video(int id)
        {
            var videoOfLecture = this.lectureService.GetVideoByLectureId(id);

            return View(videoOfLecture);
        }
    }
}