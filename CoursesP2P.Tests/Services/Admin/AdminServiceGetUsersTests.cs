using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Admin;
using CoursesP2P.Tests.Configuration;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoursesP2P.Tests.Services.Admin
{
    public class AdminServiceGetUsersTests
    {

        private CoursesP2PDbContext db;
        private IMapper mapper;
        private AdminService adminService;

        public AdminServiceGetUsersTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            this.adminService = new AdminService(db, mapper);
        }

        [Fact]
        public void GetUsersShouldReturnAllUsers()
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

            this.db.Users.AddRange(users);
            this.db.SaveChanges();

            var testUsers = this.adminService.GetUsers().ToList();

            Assert.Equal(2, testUsers.Count());
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
