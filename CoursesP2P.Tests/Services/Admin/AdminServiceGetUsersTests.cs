using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Admin;
using CoursesP2P.Services.Mapping;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels;
using System;
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
            var users = new List<User>
            {
                new User
                {
                    Id = "1",
                    FirstName = "Test1",
                    LastName = "Test2",
                    Birthday = DateTime.Today,
                    City = "Test"

                },
                new User
                {
                    Id = "2",
                    FirstName = "Test1",
                    LastName = "Test2",
                    Birthday = DateTime.Today,
                    City = "Test"
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
