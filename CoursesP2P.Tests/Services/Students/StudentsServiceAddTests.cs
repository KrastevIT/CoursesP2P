using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Students;
using CoursesP2P.Tests.Configuration;
using Xunit;

namespace CoursesP2P.Tests.Services.Students
{
    public class StudentsServiceAddTests
    {
        private CoursesP2PDbContext db;
        private StudentsService studentsService;

        public StudentsServiceAddTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.studentsService = new StudentsService(this.db);
        }

        [Theory]
        [InlineData(1, "1")]
        public void AddReturnCorrectly(int courseId, string userId)
        {
            var student = new User
            {
                Id = "1",
            };

            var instructor = new User
            {
                Id = "2"
            };

            var course = new Course
            {
                Id = 1,
                InstructorId = instructor.Id
            };



            this.db.Courses.Add(course);
            this.db.Users.Add(student);
            this.db.Users.Add(instructor);
            this.db.SaveChanges();

            var isValid = this.studentsService.Add(courseId, userId);

            Assert.True(isValid);
        }
    }
}
