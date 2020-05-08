using CoursesP2P.Models;
using CoursesP2P.Services.AzureMedia;
using CoursesP2P.Services.Lectures;
using CoursesP2P.ViewModels.Lectures.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    [Authorize]
    public class LecturesController : Controller
    {
        private readonly ILecturesService lectureService;
        private readonly IAzureMediaService azureMediaService;
        private readonly UserManager<User> userManager;

        public LecturesController(
            ILecturesService lectureService,
            IAzureMediaService azureMediaService,
            UserManager<User> userManager)
        {
            this.lectureService = lectureService;
            this.azureMediaService = azureMediaService;
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
        public async Task<IActionResult> Add(AddLecturesBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json("invalid");
            }

            var inputAsset = await this.azureMediaService.CreateInputAssetAsync(model.Video);

            var outputAsset = await this.azureMediaService.CreateOutputAssetAsync();

            var transform = await this.azureMediaService.GetOrCreateTransformAsync();

            var job = await this.azureMediaService.SubmitJobAsync(inputAsset.Name, outputAsset.Name, transform.Name);

            var waitForJobToFinish = await this.azureMediaService.WaitForJobToFinishAsync(transform.Name, job.Name);

            var streamingLocator = await this.azureMediaService.CreateStreamingLocatorAsync(outputAsset.Name);

            var getStreamingUrls = await this.azureMediaService.GetStreamingUrlsAsync(streamingLocator.Name);

            await this.azureMediaService.CleanUpAsync(transform.Name, inputAsset.Name);

            await this.lectureService.SaveLectureDbAsync(model.CourseId, model.Name,outputAsset.Name ,getStreamingUrls[2]);

            return Json("valid");
        }

        public async Task<IActionResult> Video(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var isAdmin = await this.userManager.IsInRoleAsync(user, "Administrator");
            var videoOfLecture = this.lectureService.GetVideoByLectureId(id, user.Id, isAdmin);

            return View(videoOfLecture);
        }
    }
}