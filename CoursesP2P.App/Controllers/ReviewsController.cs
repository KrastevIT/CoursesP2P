using CoursesP2P.Models;
using CoursesP2P.Services.AzureMedia;
using CoursesP2P.Services.Reviews;
using CoursesP2P.ViewModels.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IAzureStorageBlob azureMediaService;
        private readonly IReviewService reviewService;
        private readonly UserManager<User> userManager;

        public ReviewsController(
            IAzureStorageBlob azureMediaService,
            IReviewService reviewService,
            UserManager<User> userManager)
        {
            this.azureMediaService = azureMediaService;
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Index(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var model = this.reviewService.GetReviewBindingModelWithCourseId(id, userId);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1200000000)]
        [RequestSizeLimit(1200000000)]
        public async Task<IActionResult> Add(ReviewBindingModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            var modelValid = this.reviewService.GetReviewBindingModelWithCourseId(model.CourseId, userId);
            if (!ModelState.IsValid || modelValid == null)
            {
                return Json("invalid");
            }

            var inputAsset = await this.azureMediaService.CreateInputAssetAsync(model.Video);

            var outputAsset = await this.azureMediaService.CreateOutputAssetAsync();

            var transform = await this.azureMediaService.GetOrCreateTransformAsync();

            var job = await this.azureMediaService.SubmitJobAsync(inputAsset.Name, outputAsset.Name, transform.Name);

            await this.azureMediaService.WaitForJobToFinishAsync(transform.Name, job.Name);

            var streamingLocator = await this.azureMediaService.CreateStreamingLocatorAsync(outputAsset.Name);

            var getStreamingUrls = await this.azureMediaService.GetStreamingUrlsAsync(streamingLocator.Name);

            await this.azureMediaService.CleanUpAsync(transform.Name, inputAsset.Name);

            await this.reviewService.SaveReviewDbAsync(model.CourseId, outputAsset.Name, getStreamingUrls[2]);

            return Json("valid");
        }
    }
}
