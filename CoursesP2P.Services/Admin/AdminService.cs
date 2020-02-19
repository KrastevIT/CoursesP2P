using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.ViewModels.Admin;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CoursesP2P.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly CoursesP2PDbContext db;
        private readonly IMapper mapper;

        public AdminService(CoursesP2PDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<AdminUserViewModel> GetUsers()
        {
            var users = this.db.Users
                .Include(x => x.CreatedCourses)
                .Include(x => x.EnrolledCourses)
                .ToList();

            var models = this.mapper.Map<IEnumerable<AdminUserViewModel>>(users);

            foreach (var user in models)
            {
                user.Sales = user.CreatedCourses.Select(x => x.Orders).Sum();
            }

            return models;
        }

        public IEnumerable<CourseViewModel> GetCreatedCoursesByUserId(string id)
        {
            var courses = this.db.Users
                .Where(x => x.Id == id)
                .SelectMany(x => x.CreatedCourses)
                .ToList();

            var models = this.mapper.Map<IEnumerable<CourseViewModel>>(courses);

            return models;
        }

        public IEnumerable<CourseViewModel> GetEnrolledCoursesByUserId(string id)
        {
            var courses = this.db.StudentCourses
                .Where(x => x.StudentId == id)
                .Include(x => x.Course)
                .ThenInclude(x => x.Lectures)
                .Select(x => x.Course)
                .ToList();

            var models = this.mapper.Map<IEnumerable<CourseViewModel>>(courses);

            return models;
        }
    }
}
