using CoursesP2P.Models;
using CoursesP2P.Services.Students;
using CoursesP2P.ViewModels.FiveStars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly IStudentsService studentService;
        private readonly UserManager<User> userManager;

        public StudentsController(IStudentsService studentService, UserManager<User> userManager)
        {
            this.studentService = studentService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);
            var courses = this.studentService.GetMyCourses(userId);

            return View(courses);
        }

        public async Task<IActionResult> Add(int id)
        {
            var student = await this.userManager.GetUserAsync(this.User);
            var successfully = this.studentService.Add(id, student.Id);
            if (!successfully)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Rating(RatingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", model);
            }
            this.studentService.AddRating(model);
            return RedirectToAction("Index", model);
        }
    }
}