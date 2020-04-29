using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Admin;
using CoursesP2P.Services.Mapping;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

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
        public async Task GetUsersShouldReturnAllUsers()
        {
            var users = new List<User>
            {
                new User
                {
                    Id = "1",
                },
                new User
                {
                    Id = "2",
                }
            };

            await this.db.Users.AddRangeAsync(users);
            await this.db.SaveChangesAsync();

            var actual = this.adminService.GetUsers();

            Assert.Equal(2, actual.Count());
        }

        [Fact]
        public async Task GetUsersShouldReturnCreatedCourses()
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

            await this.db.Users.AddAsync(users);
            await this.db.SaveChangesAsync();

            var testUsers = this.adminService.GetUsers();

            var actual = testUsers.Select(x => x.CreatedCourses.Count()).Sum();

            Assert.Equal(2, actual);
        }

        [Fact]
        public async Task GetUsersShouldReturnEnrolledCourses()
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

            await this.db.Users.AddAsync(users);
            await this.db.SaveChangesAsync();

            var testUsers = this.adminService.GetUsers();

            var actual = testUsers.Select(x => x.EnrolledCourses.Count()).Sum();

            Assert.Equal(2, actual);
        }

        [Fact]
        public async Task GetUsersWithSales()
        {
            var course = new List<Course>
            {
                new Course
                {
                    Orders = 1
                },
                 new Course
                {
                    Orders = 2
                }
            };

            var user = new User
            {
                CreatedCourses = course
            };

            await this.db.Users.AddAsync(user);
            await this.db.SaveChangesAsync();

            var actual = this.adminService.GetUsers().Select(x => x.Sales).FirstOrDefault();

            Assert.Equal(3, actual);
        }
    }
}
