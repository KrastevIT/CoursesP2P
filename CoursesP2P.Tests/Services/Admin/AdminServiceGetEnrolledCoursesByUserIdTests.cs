using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Admin;
using CoursesP2P.Tests.Configuration;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task GetEnrolledCoursesByUserIdReturnEnrolledCourses(string id)
        {
            var course = new Course
            {
                Id = 1
            };

            await db.Courses.AddAsync(course);
            await db.SaveChangesAsync();

            var user = new User
            {
                Id = "1",
            };

            await this.db.Users.AddAsync(user);
            await this.db.SaveChangesAsync();

            var studentCourse = new StudentCourse
            {
                CourseId = course.Id,
                StudentId = user.Id
            };

            await this.db.StudentCourses.AddAsync(studentCourse);
            await this.db.SaveChangesAsync();

            var testUsers = this.adminService.GetEnrolledCoursesByUserId(id);

            var actual = testUsers.Count();

            Assert.Equal(1, actual);
        }
    }
}
