using CoursesP2P.Services.AzureMedia;
using CoursesP2P.Services.Reviews;
using CoursesP2P.ViewModels.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IAzureMediaService azureMediaService;
        private readonly IReviewService reviewService;

        public ReviewsController(IAzureMediaService azureMediaService, IReviewService reviewService)
        {
            this.azureMediaService = azureMediaService;
            this.reviewService = reviewService;
        }

        [Authorize]
        public IActionResult Index(int id)
        {
            var model = new ReviewBindingModel
            {
                CourseId = id
            };
            return View(model);
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
        [RequestSizeLimit(1073741824)]
        public async Task<IActionResult> Add(ReviewBindingModel model)
        {
            if (!ModelState.IsValid)
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
