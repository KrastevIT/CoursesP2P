using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoursesP2P.App.ViewComponents
{
    public class InstructorDashbordViewComponent : ViewComponent
    {
        private readonly CoursesP2PDbContext db;
        private readonly UserManager<User> userManager;

        public InstructorDashbordViewComponent(
            CoursesP2PDbContext db,
            UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userId = this.userManager.GetUserId(this.UserClaimsPrincipal);

            var user = this.db.Users.Where(x => x.Id == userId)
                 .Include(x => x.CreatedCourses)
                 .FirstOrDefault();

            var profit = this.db.PaymentsToInstructors
                .Where(x => x.InstructorEmail == user.Email)
                .Select(x => x.Amount)
                .FirstOrDefault();

            var model = new InstructorDashbordViewModel
            {
                CreatedCourses = user.CreatedCourses.Count(),
                EnrolledCourses = user.CreatedCourses.Select(x => x.Orders).Sum(),
                Profit = profit
            };

            return View(model);
        }
    }
}
