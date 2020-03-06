using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Courses;
using CoursesP2P.Tests.Configuration;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CoursesP2P.Tests.Services.Courses
{
    public class CoursesServiceGetCourseByIdTests
    {
        private CoursesP2PDbContext db;
        private IMapper mapper;
        private UserManager<User> userManager;
        private CoursesService coursesService;

        public CoursesServiceGetCourseByIdTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            this.userManager = UserManagerMock.UserManagerMockTest();
            var cloudinary = new Mock<ICloudinaryService>().Object;
            this.coursesService = new CoursesService(this.db, this.mapper, this.userManager, cloudinary);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void GetCourseByIdRetunrCourseById(int id, int expected)
        {
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1
                },

                new Course
                {
                    Id = 2
                }
            };

            this.db.Courses.AddRange(courses);
            this.db.SaveChanges();

            var getCourse = this.coursesService.GetCourseById(id);

            Assert.Equal(expected, getCourse.Id);
        }

        [Theory]
        [InlineData(3)]
        public void GetCourseByIdWithNonExistentCourseIdReturnException(int id)
        {
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1
                },

                new Course
                {
                    Id = 2
                }
            };

            this.db.Courses.AddRange(courses);
            this.db.SaveChanges();

            Assert.Throws<ArgumentNullException>(() =>
            {
                this.coursesService.GetCourseById(id);
            });
        }
    }
}
