using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Admin;
using CoursesP2P.Tests.Configuration;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoursesP2P.Tests
{
    public class AdminServiceTests
    {
        [Fact]
        public void GetUsersShouldReturnAllUsers()
        {
            var db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var mapper = MapperMock.AutoMapperMock();

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

            db.Users.AddRange(users);
            db.SaveChanges();

            var adminService = new AdminService(db, mapper);

            var testUsers = adminService.GetUsers().ToList();

            Assert.Equal(2, testUsers.Count());
        }

        [Fact]
        public void GetUsersShouldReturnCreatedCourses()
        {
            var db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var mapper = MapperMock.AutoMapperMock();

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

            db.Users.Add(users);
            db.SaveChanges();

            var adminService = new AdminService(db, mapper);

            var testUsers = adminService.GetUsers();

            var actual = testUsers.Select(x => x.CreatedCourses.Count()).Sum();

            Assert.Equal(2, actual);
        }

        [Fact]
        public void GetUsersShouldReturnEnrolledCourses()
        {
            var db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var mapper = MapperMock.AutoMapperMock();

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

            db.Users.Add(users);
            db.SaveChanges();

            var adminService = new AdminService(db, mapper);

            var testUsers = adminService.GetUsers();

            var actual = testUsers.Select(x => x.EnrolledCourses.Count()).Sum();

            Assert.Equal(2, actual);
        }

        [Theory]
        [InlineData("1")]
        public void GetCreatedCoursesByUserIdReturnCreatedCourses(string id)
        {
            var db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var mapper = MapperMock.AutoMapperMock();

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

            db.Users.Add(users);
            db.SaveChanges();

            var adminService = new AdminService(db, mapper);

            var testUsers = adminService.GetCreatedCoursesByUserId(id);

            var actual = testUsers.Count();

            Assert.Equal(2, actual);
        }

        [Theory]
        [InlineData("1")]
        public void GetEnrolledCoursesByUserIdReturnEnrolledCourses(string id)
        {
            var db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var mapper = MapperMock.AutoMapperMock();

            var course = new Course
            {
                Id = 1
            };

            db.Courses.Add(course);
            db.SaveChanges();

            var user = new User
            {
                Id = "1",
            };

            db.Users.Add(user);
            db.SaveChanges();

            var studentCourse = new StudentCourse
            {
                CourseId = course.Id,
                StudentId = user.Id
            };

            db.StudentCourses.Add(studentCourse);
            db.SaveChanges();

            var adminService = new AdminService(db, mapper);

            var testUsers = adminService.GetEnrolledCoursesByUserId(id);

            var actual = testUsers.Count();

            Assert.Equal(1, actual);
        }
    }
}
