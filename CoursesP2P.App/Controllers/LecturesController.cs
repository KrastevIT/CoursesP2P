using CoursesP2P.Models;
using CoursesP2P.Services.Lectures;
using CoursesP2P.ViewModels.Lectures.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            var isAdmin = await this.userManager.IsInRoleAsync(user, "Administrator");
            var lectures = this.lectureService.GetLecturesByCourse(id, user.Id, isAdmin);

            this.ViewData["id"] = id;

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
        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
        [RequestSizeLimit(1073741824)]
        public async Task Add(AddLecturesBindingModel model)
        {
           

            // if (!ModelState.IsValid)
            // {
            //     return View(model);
            // }

            //await this.lectureService.AddAsync(model);

            //return RedirectToAction("Index", "Instructors");
        }

        public async Task<IActionResult> Video(int id)
        {
            var user =  await this.userManager.GetUserAsync(this.User);
            var isAdmin = await this.userManager.IsInRoleAsync(user, "Administrator");
            var videoOfLecture = this.lectureService.GetVideoByLectureId(id, user.Id, isAdmin);

            return View(videoOfLecture);
        }
    }
}