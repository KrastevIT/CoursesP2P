using CoursesP2P.Services.Lectures;
using CoursesP2P.ViewModels.Lectures.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    [Authorize]
    public class LecturesController : Controller
    {
        private readonly ILectureService lectureService;

        public LecturesController(ILectureService lectureService)
        {
            this.lectureService = lectureService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var lectures = await this.lectureService.GetLecturesByCourseIdAsync(id, this.User);

            return View(lectures);
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
        public IActionResult Add(AddLecturesBindingModel model)
        {
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