using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Students;
using CoursesP2P.Tests.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoursesP2P.Tests.Services.Students
{
    public class StudentsServiceGetMyCoursesAsyncTests
    {
        private CoursesP2PDbContext db;
        private StudentsService studentsService;

        public StudentsServiceGetMyCoursesAsyncTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.studentsService = new StudentsService(this.db);
        }

        [Fact]
        public async Task GetMyCoursesAsyncReturnCorrectly()
        {
            var course = new Course
            {
                Id = 1
            };

            var user = new User
            {
                Id = "1",
            };

            await this.db.Courses.AddAsync(course);
            await this.db.Users.AddAsync(user);
            await this.db.SaveChangesAsync();

            var studentCourse = new StudentCourse
            {
                CourseId = course.Id,
                StudentId = user.Id
            };

            await this.db.StudentCourses.AddAsync(studentCourse);
            await this.db.SaveChangesAsync();

            var actual = this.studentsService.GetMyCourses("1").Count();

            Assert.Equal(1, actual);
        }
    }
}
