using CoursesP2P.Data;
using CoursesP2P.Services.Courses;
using CoursesP2P.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoursesP2P.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly CoursesP2PDbContext db;
        private readonly ICoursesService coursesService;

        public HomeService(CoursesP2PDbContext db, ICoursesService coursesService)
        {
            this.db = db;
            this.coursesService = coursesService;
        }

        public HomeInfoAndCoursesViewModel GetAllInfoWithCourses()
        {
            var coursesModel = this.coursesService.GetAllCourses();

            var students = this.db.Users.Count();
            var instructors = this.db.Users.Include(x => x.CreatedCourses).Where(x => x.CreatedCourses.Any()).Count();
            var courses = this.db.Courses.Count();
            var lectures = this.db.Courses.Include(x => x.Lectures).SelectMany(x => x.Lectures).Count();

            var model = new HomeInfoAndCoursesViewModel
            {
                AllCourses = coursesModel,
                Students = students,
                Instructors = instructors,
                Courses = courses,
                Lectures = lectures
            };

            return model;
        }
    }
}
