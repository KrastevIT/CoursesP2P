using CoursesP2P.Data;
using CoursesP2P.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoursesP2P.App.ViewComponents
{
    public class HomeAllInfoViewComponent : ViewComponent
    {
        private readonly CoursesP2PDbContext db;

        public HomeAllInfoViewComponent(CoursesP2PDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            var students = this.db.Users.Count();
            var instructors = this.db.Users.Include(x => x.CreatedCourses).Where(x => x.CreatedCourses.Any()).Count();
            var courses = this.db.Courses.Count();
            //var lectures = this.db.Courses.Include(x => x.Lectures).SelectMany(x => x.Lectures).Count();
            var lectures = this.db.Courses.Where(x => x.Status == false).SelectMany(x => x.Lectures).Count();

            var model = new HomeAllInfoViewModel
            {
                Students = students,
                Instructors = instructors,
                Courses = courses,
                Lectures = lectures
            };

            return View(model);
        }
    }
}
