using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Admin;
using CoursesP2P.Tests.Configuration;
using System.Linq;
using Xunit;

namespace CoursesP2P.Tests.Services.Admin
{
    public class AdminServiceGetEnrolledCoursesByUserIdTests
    {
        private CoursesP2PDbContext db;
        private AdminService adminService;

        public AdminServiceGetEnrolledCoursesByUserIdTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.adminService = new AdminService(db);
        }

        [Theory]
        [InlineData("1")]
        public void GetEnrolledCoursesByUserIdReturnEnrolledCourses(string id)
        {
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

            this.db.Users.Add(user);
            this.db.SaveChanges();

            var studentCourse = new StudentCourse
            {
                CourseId = course.Id,
                StudentId = user.Id
            };

            this.db.StudentCourses.Add(studentCourse);
            this.db.SaveChanges();

            var testUsers = this.adminService.GetEnrolledCoursesByUserId(id);

            var actual = testUsers.Count();

            Assert.Equal(1, actual);
        }
    }
}
