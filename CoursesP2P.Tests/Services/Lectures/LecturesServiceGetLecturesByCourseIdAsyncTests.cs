using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Lectures;
using CoursesP2P.Tests.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoursesP2P.Tests.Services.Lectures
{
    public class LecturesServiceGetLecturesByCourseIdAsyncTests
    {
        private CoursesP2PDbContext db;
        private LecturesService lecturesService;

        public LecturesServiceGetLecturesByCourseIdAsyncTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var cloudinary = new Mock<ICloudinaryService>().Object;

            this.lecturesService = new LecturesService(this.db, cloudinary);
        }

        [Theory]
        [InlineData(1, 2)]
        public async Task GetLecturesByCourseIdAsyncReturnCorrectly(int courseId, int expected)
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

            await this.db.Users.AddAsync(user);
            await this.db.Courses.AddAsync(course);
            await this.db.SaveChangesAsync();

            var studentCourse = new StudentCourse
            {
                CourseId = course.Id,
                StudentId = user.Id
            };

            await this.db.StudentCourses.AddAsync(studentCourse);
            await this.db.SaveChangesAsync();


            var actual = this.lecturesService.GetLecturesByCourse(courseId, "1", false).Count();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2)]
        public async Task GetLecturesByCourseIdAsyncWithInvalidIdReturnExceptions(int courseId)
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

            await this.db.Users.AddAsync(user);
            await this.db.Courses.AddAsync(course);
            await this.db.SaveChangesAsync();

            var studentCourse = new StudentCourse
            {
                CourseId = course.Id,
                StudentId = user.Id
            };

            await this.db.StudentCourses.AddAsync(studentCourse);
            await this.db.SaveChangesAsync();


            Assert.Throws<InvalidOperationException>(() => this.lecturesService.GetLecturesByCourse(courseId, "1", false));
        }
    }
}
