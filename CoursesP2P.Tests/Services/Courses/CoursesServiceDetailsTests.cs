using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Courses;
using CoursesP2P.Services.Mapping;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CoursesP2P.Tests.Services.Courses
{
    public class CoursesServiceDetailsTests
    {
        private CoursesP2PDbContext db;
        private CoursesService coursesService;

        public CoursesServiceDetailsTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var cloudinary = new Mock<ICloudinaryService>().Object;

            this.coursesService = new CoursesService(this.db, null);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public async Task DetailsReturnCorrectly(int id, int expected)
        {
            var courses = new List<Course>
            {
               new Course
               {
                   Id = 1,
                   Skills = "skillOne *skilTwo"
               },
               new Course
               {
                   Id = 2,
               }
            };

            await this.db.Courses.AddRangeAsync(courses);
            await this.db.SaveChangesAsync();

            var actual = this.coursesService.Details(id);

            Assert.Equal(expected, actual.Id);
        }

        [Theory]
        [InlineData(3)]
        public async Task DetailsWithInvalidIdReturnException(int id)
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

           await this.db.Courses.AddRangeAsync(courses);
           await this.db.SaveChangesAsync();

            Assert.Throws<ArgumentNullException>(() => this.coursesService.Details(id));
        }

        [Theory]
        [InlineData(1, 2)]
        public async Task DetailsReturnCourseWithLectures(int id, int expected)
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

            var courses = new List<Course>
            {
               new Course
               {
                   Id = 1,
                   Skills = "skillOne",
                   Lectures = lectures
               },
               new Course
               {
                   Id = 2,
                   Skills = "skillTwo"
               }
            };

            await this.db.Courses.AddRangeAsync(courses);
            await this.db.SaveChangesAsync();

            var actual = this.coursesService.Details(id).LectureName.Count;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 1)]
        public async Task DetailsReturnCourseWithSkills(int id, int expected)
        {
            var courses = new List<Course>
            {
               new Course
               {
                   Id = 1,
                   Skills = "skillOne"
               },
            };

            await this.db.Courses.AddRangeAsync(courses);
            await this.db.SaveChangesAsync();

            var actual = this.coursesService.Details(id);

            Assert.Equal(expected, actual.Id);
        }

    }
}
