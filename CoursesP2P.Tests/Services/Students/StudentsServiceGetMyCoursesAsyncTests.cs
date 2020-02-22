using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Students;
using CoursesP2P.Tests.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Xunit;

namespace CoursesP2P.Tests.Services.Students
{
    public class StudentsServiceGetMyCoursesAsyncTests
    {
        private CoursesP2PDbContext db;
        private readonly IMapper mapper;
        private UserManager<User> userManager;
        private StudentsService studentsService;

        public StudentsServiceGetMyCoursesAsyncTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            this.userManager = UserManagerMock.UserManagerMockTest();
            this.studentsService = new StudentsService(this.db, this.mapper, this.userManager);
        }

        [Fact]
        public void GetMyCoursesAsyncReturnCorrectly()
        {
            var course = new Course
            {
                Id = 1
            };

            var user = new User
            {
                Id = "1",
            };

            this.db.Courses.Add(course);
            this.db.Users.Add(user);
            this.db.SaveChanges();

            var studentCourse = new StudentCourse
            {
                CourseId = course.Id,
                StudentId = user.Id
            };

            this.db.StudentCourses.Add(studentCourse);
            this.db.SaveChanges();

            var actual = this.studentsService.GetMyCoursesAsync(user).ToList().Count;

            Assert.Equal(1, actual);
        }
    }
}
