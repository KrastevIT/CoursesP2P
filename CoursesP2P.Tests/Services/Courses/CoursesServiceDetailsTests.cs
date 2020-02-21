using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Courses;
using CoursesP2P.Tests.Configuration;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CoursesP2P.Tests.Services.Courses
{
    public class CoursesServiceDetailsTests
    {
        private CoursesP2PDbContext db;
        private IMapper mapper;
        private UserManager<User> userManager;
        private CoursesService coursesService;

        public CoursesServiceDetailsTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            this.userManager = UserManagerMock.UserManagerMockTest();
            this.coursesService = new CoursesService(this.db, this.mapper, this.userManager);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void DetailsReturnCorrectly(int id, int expected)
        {
            var courses = new List<Course>
            {
               new Course
               {
                   Id = 1,
                   Skills = "skillOne"
               },
               new Course
               {
                   Id = 2,
                   Skills = "skillTwo"
               }
            };

            this.db.Courses.AddRange(courses);
            this.db.SaveChanges();

            var actual = this.coursesService.Details(id);

            Assert.Equal(expected, actual.Id);
        }

        [Theory]
        [InlineData(3)]
        public void DetailsWithInvalidIdReturnException(int id)
        {
            var courses = new List<Course>
            {
               new Course
               {
                   Id = 1,
                   Skills = "skillOne"
               },
               new Course
               {
                   Id = 2,
                   Skills = "skillTwo"
               }
            };

            this.db.Courses.AddRange(courses);
            this.db.SaveChanges();

            Assert.Throws<ArgumentNullException>(() => this.coursesService.Details(id));
        }

    }
}
