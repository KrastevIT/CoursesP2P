using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Lectures;
using CoursesP2P.Tests.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CoursesP2P.Tests.Services.Lectures
{
    public class GetLectureBindingModelWithCourseIdTests
    {
        private CoursesP2PDbContext db;
        private readonly IMapper mapper;
        private LecturesService lecturesService;

        public GetLectureBindingModelWithCourseIdTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            var cloudinary = new Mock<ICloudinaryService>().Object;

            this.lecturesService = new LecturesService(this.db, this.mapper, cloudinary);
        }

        [Fact]
        public void GetLectureBindingModelWithCourseIdReturnCorrectly()
        {
            var user = new User
            {
                Id = "100"
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();

            var course = new Course
            {
                Id = 1,
                InstructorId = "100"
            };
            this.db.Courses.Add(course);
            this.db.SaveChanges();

            var actual = this.lecturesService.GetLectureBindingModelWithCourseId(1, user);

            Assert.Equal(1, actual.CourseId);
        }
    }
}
