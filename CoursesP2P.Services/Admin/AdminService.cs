using CoursesP2P.Data;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Admin;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;

namespace CoursesP2P.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly CoursesP2PDbContext db;

        public AdminService(CoursesP2PDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<AdminUserViewModel> GetUsers()
        {
            var models = this.db.Users
                .To<AdminUserViewModel>()
                .ToList();

            foreach (var user in models)
            {
                user.Sales = user.CreatedCourses.Select(x => x.Orders).Sum();
            }

            return models;
        }

        public IEnumerable<CourseViewModel> GetCreatedCoursesByUserId(string id)
        {
            var models = this.db.Users
                .Where(x => x.Id == id)
                .SelectMany(x => x.CreatedCourses)
                .To<CourseViewModel>()
                .ToList();

            return models;
        }

        public IEnumerable<CourseViewModel> GetEnrolledCoursesByUserId(string id)
        {
            var models = this.db.StudentCourses
                .Where(x => x.StudentId == id)
                .Select(x => x.Course)
                .To<CourseViewModel>()
                .ToList();


            return models;
        }
    }
}
