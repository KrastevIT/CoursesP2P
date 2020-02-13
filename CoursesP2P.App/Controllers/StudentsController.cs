using CoursesP2P.Models;
using CoursesP2P.Services.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly IStudentService studentService;
        private readonly UserManager<User> userManager;

        public StudentsController(IStudentService studentService, UserManager<User> userManager)
        {
            this.studentService = studentService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await this.studentService.GetMyCourses(this.User);
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
    }
}