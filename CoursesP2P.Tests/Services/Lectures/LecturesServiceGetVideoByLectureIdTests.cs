using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Lectures;
using CoursesP2P.Tests.Configuration;
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

            this.lecturesService = new LecturesService(this.db, this.mapper);
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

            this.db.Courses.Add(course);
            this.db.SaveChanges();

           var model = this.lecturesService.GetVideoByLectureId(id);

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

            this.db.Courses.Add(course);
            this.db.SaveChanges();

            Assert.Throws<ArgumentNullException>(() => this.lecturesService.GetVideoByLectureId(id));
        }
    }
}
