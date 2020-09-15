using CoursesP2P.Models;
using CoursesP2P.Services.AzureMedia;
using CoursesP2P.Services.AzureStorageBlob;
using CoursesP2P.Services.Courses;
using CoursesP2P.Services.Reviews;
using CoursesP2P.ViewModels.Courses.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    public class CoursesController : Controller
    {
        private const int ItemsPerPage = 6;

        private readonly ICoursesService coursesService;
        private readonly IReviewService reviewService;
        private readonly UserManager<User> userManager;
        private readonly IAzureStorageBlobService azureStorageBlobService;
        private readonly IAzureStorageBlob azureMediaService;

        public CoursesController(
            ICoursesService coursesService,
            IReviewService reviewService,
            UserManager<User> userManager,
            IAzureStorageBlobService azureStorageBlobService,
            IAzureStorageBlob azureMediaService)
        {
            this.coursesService = coursesService;
            this.reviewService = reviewService;
            this.userManager = userManager;
            this.azureStorageBlobService = azureStorageBlobService;
            this.azureMediaService = azureMediaService;
        }

        public IActionResult Category(string name, int page = 1)
        {
            var model = this.coursesService.GetCategoryDetails(name, page);
            model.Courses = this.coursesService.GetCoursesByCategory(name, ItemsPerPage, (page - 1) * ItemsPerPage);

            var coursesByCategory = this.coursesService.GetCoursesByCategory(name, ItemsPerPage, (page - 1) * ItemsPerPage);
            if (coursesByCategory == null)
            {
                return NotFound();
            }
            this.ViewData["category"] = name.Replace("_", " ");
            return View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1200000000)]
        [RequestSizeLimit(1200000000)]
        public async Task<IActionResult> Create(CreateCourseBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Alert = "Невалидни Данни!";
                return Json("Invalid");
            }

            try
            {
                var inputAsset = await this.azureMediaService.CreateInputAssetAsync(model.Video);

                var outputAsset = await this.azureMediaService.CreateOutputAssetAsync();

                var transform = await this.azureMediaService.GetOrCreateTransformAsync();

                var job = await this.azureMediaService.SubmitJobAsync(inputAsset.Name, outputAsset.Name, transform.Name);

                await this.azureMediaService.WaitForJobToFinishAsync(transform.Name, job.Name);

                var streamingLocator = await this.azureMediaService.CreateStreamingLocatorAsync(outputAsset.Name);

                var getStreamingUrls = await this.azureMediaService.GetStreamingUrlsAsync(streamingLocator.Name);

                await this.azureMediaService.CleanUpAsync(transform.Name, inputAsset.Name);

                var user = await this.userManager.GetUserAsync(this.User);

                var imageUrl = await this.azureStorageBlobService.UploadImageAsync(model.Image);

                var courseId = await this.coursesService.CreateAsync(model, user.Id, user.FirstName, user.LastName, imageUrl);

                await this.reviewService.SaveReviewDbAsync(courseId, outputAsset.Name, getStreamingUrls[2]);

                return Json("Valid");
            }
            catch
            {
                throw new InvalidOperationException("Неуспешно добаване в AzureMedia");
            }

        }

        public IActionResult Details(int id)
        {
            var course = this.coursesService.Details(id);
            return View(course);
        }
    }
}