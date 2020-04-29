using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Lectures;
using CoursesP2P.Services.Mapping;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CoursesP2P.Tests.Services.Lectures
{
    public class LecturesServiceGetVideoByLectureIdTests
    {
        private CoursesP2PDbContext db;
        private LecturesService lecturesService;

        public LecturesServiceGetVideoByLectureIdTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var cloudinary = new Mock<ICloudinaryService>().Object;

            this.lecturesService = new LecturesService(this.db, cloudinary);
        }

        [Theory]
        [InlineData(2)]
        public async Task GetVideoByLectureIdReturnCorrectly(int id)
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
                Id = 10,
                Lectures = lectures
            };

            var user = new User
            {
                Id = "1",
            };

            var studentCourse = new StudentCourse
            {
                CourseId = 10,
                StudentId = "1"
            };

            await this.db.Users.AddAsync(user);
            await this.db.StudentCourses.AddAsync(studentCourse);
            await this.db.Courses.AddAsync(course);
            await this.db.SaveChangesAsync();

            var model = this.lecturesService.GetVideoByLectureId(id, "1", false);

            Assert.NotNull(model);
        }

        [Theory]
        [InlineData(3)]
        public async Task GetVideoByLectureIdWithInvalidIdReturnException(int id)
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
                Id = 10,
                Lectures = lectures
            };

            var user = new User
            {
                Id = "1"
            };

            await this.db.Courses.AddAsync(course);
            await this.db.SaveChangesAsync();

            Assert.Throws<InvalidOperationException>(() => this.lecturesService.GetVideoByLectureId(id, "1", false));
        }
    }
}
