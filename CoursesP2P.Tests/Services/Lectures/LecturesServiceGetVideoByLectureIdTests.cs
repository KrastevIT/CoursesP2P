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
using System.Text;
using Xunit;

namespace CoursesP2P.Tests.Services.Lectures
{
    public class LecturesServiceGetVideoByLectureIdTests
    {
        private CoursesP2PDbContext db;
        private readonly IMapper mapper;
        private LecturesService lecturesService;

        public LecturesServiceGetVideoByLectureIdTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            var cloudinary = new Mock<ICloudinaryService>().Object;

            this.lecturesService = new LecturesService(this.db, this.mapper, cloudinary);
        }

        [Theory]
        [InlineData(2)]
        public void GetVideoByLectureIdReturnCorrectly(int id)
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

            this.db.Courses.Add(course);
            this.db.SaveChanges();

            var model = this.lecturesService.GetVideoByLectureId(id, user);

            Assert.NotNull(model);
        }

        [Theory]
        [InlineData(3)]
        public void GetVideoByLectureIdWithInvalidIdReturnException(int id)
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

            this.db.Courses.Add(course);
            this.db.SaveChanges();

            Assert.Throws<ArgumentNullException>(() => this.lecturesService.GetVideoByLectureId(id, user));
        }
    }
}
