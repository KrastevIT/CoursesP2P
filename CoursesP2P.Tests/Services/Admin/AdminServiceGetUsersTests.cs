using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Admin;
using CoursesP2P.Services.Mapping;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels;
using CoursesP2P.ViewModels.Admin;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace CoursesP2P.Tests.Services.Admin
{
    public class AdminServiceGetUsersTests
    {

        private CoursesP2PDbContext db;
        private AdminService adminService;

        public AdminServiceGetUsersTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.adminService = new AdminService(db);
        }

        [Fact]
        public void GetUsersShouldReturnAllUsers()
        {
            var user = new User
            {
              Id = "1"
            };

            var create = new List<Course>
            {
                new Course
                {
                    Id = 1
                }
            };

            var enrolled = new List<StudentCourse>
            {
                new StudentCourse
                {
                    CourseId = 1,
                    StudentId = "1"
                }
            };

            user.CreatedCourses = create;
            user.EnrolledCourses = enrolled;

            this.db.Users.Add(user);
            this.db.SaveChanges();

            var actual = this.adminService.GetUsers();

            Assert.Equal(1, actual.Count());
        }

        [Fact]
        public void GetUsersShouldReturnCreatedCourses()
        {
            var course = new List<Course>
            {
                new Course
                {
                    Id = 1
                },
                new Course
                {
                  Id = 2
                }
            };

            var users = new User
            {
                Id = "1",
                CreatedCourses = course
            };

            this.db.Users.Add(users);
            this.db.SaveChanges();

            var testUsers = this.adminService.GetUsers();

            var actual = testUsers.Select(x => x.CreatedCourses.Count()).Sum();

            Assert.Equal(2, actual);
        }

        [Fact]
        public void GetUsersShouldReturnEnrolledCourses()
        {
            var studentCourse = new List<StudentCourse>
            {
                new StudentCourse
                {
                    CourseId = 1,
                    StudentId = "1"
                },
                new StudentCourse
                {
                  CourseId = 2,
                    StudentId = "2"
                }
            };

            var users = new User
            {
                Id = "1",
                EnrolledCourses = studentCourse
            };

            this.db.Users.Add(users);
            this.db.SaveChanges();

            var testUsers = this.adminService.GetUsers();

            var actual = testUsers.Select(x => x.EnrolledCourses.Count()).Sum();

            Assert.Equal(2, actual);
        }
    }
}
