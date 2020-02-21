using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Lectures;
using CoursesP2P.Tests.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CoursesP2P.Tests.Services.Lectures
{
    public class LecturesServiceGetLecturesByCourseIdAsyncTests
    {
        private CoursesP2PDbContext db;
        private readonly IMapper mapper;
        private UserManager<User> userManager;
        private LecturesService lecturesService;

        public LecturesServiceGetLecturesByCourseIdAsyncTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            this.userManager = UserManagerMock.UserManagerMockTest();
            var mockEnvironment = new Mock<IWebHostEnvironment>().Object;

            this.lecturesService = new LecturesService(this.db, this.mapper, this.userManager, mockEnvironment);

        }

        [Theory]
        [InlineData(1, 2)]
        public void GetLecturesByCourseIdAsync(int courseId, int expected)
        {
           


            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1
                },
                new Lecture
                {
                    Id = 2
                }
            };

            var course = new Course
            {
                Id = 1,
                Lectures = lectures
            };

            var user = new User
            {
                Id = "1",
            };

            this.db.Users.Add(user);
            this.db.Courses.Add(course);
            this.db.SaveChanges();

            var studentCourse = new StudentCourse
            {
                CourseId = course.Id,
                StudentId = user.Id
            };

            this.db.StudentCourses.Add(studentCourse);
            this.db.SaveChanges();

           
            var actual = this.lecturesService.GetLecturesByCourseIdAsync(courseId, user).Count();

            Assert.Equal(expected, actual);
        }
    }
}
