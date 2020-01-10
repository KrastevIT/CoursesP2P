using CoursesP2P.App.Models.ViewModels;
using CoursesP2P.Data;
using CoursesP2P.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.App.Controllers
{
    public class LecturersController : Controller
    {
        private readonly CoursesP2PDbContext coursesP2PDbContext;
        private readonly UserManager<User> userManager;

        public LecturersController(CoursesP2PDbContext coursesP2PDbContext, UserManager<User> userManager)
        {
            this.coursesP2PDbContext = coursesP2PDbContext;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var lecturer = await this.userManager.GetUserAsync(this.User);

            var courses = coursesP2PDbContext.Courses.Where(x => x.InstructorId == lecturer.Id).ToList();

            var models = new List<CourseViewModel>();

            foreach (var course in courses)
            {
                var model = new CourseViewModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    LecturerFullName = lecturer.FirstName + ' ' + lecturer.LastName,
                    Price = course.Price,
                    Category = course.Category,
                    Image = course.Image
                };

                models.Add(model);
            }

            return View(models);
        }
    }
}