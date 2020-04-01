using CoursesP2P.Models;
using CoursesP2P.Services.Students;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoursesP2P.App.ViewComponents
{
    public class RatingViewComponent : ViewComponent
    {
        private readonly IStudentsService studentsService;
        private readonly UserManager<User> userManager;

        public RatingViewComponent(IStudentsService studentsService, UserManager<User> userManager)
        {
            this.studentsService = studentsService;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke(int courseId)
        {
            var studentId = this.userManager.GetUserId(this.UserClaimsPrincipal);
            var model = this.studentsService.GetRating(studentId, courseId);
            model.CourseId = courseId;
            return View(model);
        }
    }
}
