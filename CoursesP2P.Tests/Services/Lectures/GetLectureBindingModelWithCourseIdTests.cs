using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Lectures;
using CoursesP2P.Tests.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoursesP2P.Tests.Services.Lectures
{
    public class GetLectureBindingModelWithCourseIdTests
    {
        private CoursesP2PDbContext db;
        private LecturesService lecturesService;

        public GetLectureBindingModelWithCourseIdTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.lecturesService = new LecturesService(this.db);
        }

        [Fact]
        public async Task GetLectureBindingModelWithCourseIdReturnCorrectly()
        {
            var user = new User
            {
                Id = "100"
            };

           await this.db.Users.AddAsync(user);
           await this.db.SaveChangesAsync();

            var course = new Course
            {
                Id = 1,
                InstructorId = "1"
            };
           await this.db.Courses.AddAsync(course);
           await this.db.SaveChangesAsync();

            var actual = this.lecturesService.GetLectureBindingModelWithCourseId(1, "1");

            Assert.Equal(1, actual.CourseId);
        }
    }
}
