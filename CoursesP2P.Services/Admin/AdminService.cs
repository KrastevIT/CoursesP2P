using CoursesP2P.Data;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Admin;
using CoursesP2P.ViewModels.Courses.ViewModels;
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
            var models = this.db.Users.To<AdminUserViewModel>().ToList();
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
            //var courses = this.db.StudentCourses
            //    .Where(x => x.StudentId == id)
            //    .Include(x => x.Course)
            //    .ThenInclude(x => x.Lectures)
            //    .Select(x => x.Course)
            //    .ToList();

            //var models = this.mapper.Map<IEnumerable<CourseViewModel>>(courses);

            return null;
        }
    }
}
