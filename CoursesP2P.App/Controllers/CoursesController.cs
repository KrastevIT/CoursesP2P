using CoursesP2P.Models;
using CoursesP2P.Services.AzureStorageBlob;
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
        private const int ItemsPerPage = 6;

        private readonly ICoursesService coursesService;
        private readonly UserManager<User> userManager;
        private readonly IAzureStorageBlobService azureStorageBlobService;

        public CoursesController(
            ICoursesService coursesService,
            UserManager<User> userManager,
            IAzureStorageBlobService azureStorageBlobService)
        {
            this.coursesService = coursesService;
            this.userManager = userManager;
            this.azureStorageBlobService = azureStorageBlobService;
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
        public async Task<IActionResult> Create(CreateCourseBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var imageUrl = await this.azureStorageBlobService.UploadImageAsync(model.Image);

            await this.coursesService.CreateAsync(model, user.Id, user.FirstName, user.LastName, imageUrl);

            return RedirectToAction("Index", "Instructors");
        }

        public IActionResult Details(int id)
        {
            var course = this.coursesService.Details(id);
            return View(course);
        }
    }
}