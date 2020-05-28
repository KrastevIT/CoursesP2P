using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Courses;
using CoursesP2P.Services.Mapping;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CoursesP2P.Tests.Services.Courses
{
    public class CoursesServiceGetCoursesByCategoryTests
    {
        private CoursesP2PDbContext db;
        private CoursesService coursesService;

        public CoursesServiceGetCoursesByCategoryTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.coursesService = new CoursesService(this.db);
        }

        [Theory]
        [InlineData("Програмиране", 2)]
        [InlineData("Маркетинг", 1)]
        public async Task GetCoursesByCategoryReturnCourseByCategory(string categoryName, int expected)
        {
            var categoryDevelopment = (Category)Enum.Parse(typeof(Category), "Програмиране");
            var categoryMarkiting = (Category)Enum.Parse(typeof(Category), "Маркетинг");

            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Category = categoryDevelopment,
                    Status = true
                },
                new Course
                {
                    Id = 2,
                    Category = categoryDevelopment,
                    Status = true

                },
                new Course
                {
                    Id = 3,
                    Category = categoryMarkiting,
                    Status = true
                },
            };

            await this.db.Courses.AddRangeAsync(courses);
            await this.db.SaveChangesAsync();

            var getCourses = this.coursesService.GetCoursesByCategory(categoryName);

            Assert.Equal(expected, getCourses.Count());
        }

        [Theory]
        [InlineData("InvalidCategory")]
        public async Task GetCoursesByCategoryWithNonExistentCategoryReturnException(string categoryName)
        {
            var categoryDevelopment = (Category)Enum.Parse(typeof(Category), "Програмиране");
            var categoryMarkiting = (Category)Enum.Parse(typeof(Category), "Маркетинг");

            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Category = categoryDevelopment
                },
                new Course
                {
                    Id = 2,
                    Category = categoryDevelopment
                },
                new Course
                {
                    Id = 3,
                    Category = categoryMarkiting
                },
            };

            await this.db.Courses.AddRangeAsync(courses);
            await this.db.SaveChangesAsync();


            Assert.Throws<InvalidCastException>(() => this.coursesService.GetCoursesByCategory(categoryName));
        }

        [Theory]
        [InlineData("Програмиране", 2)]
        public void GetCoursesByCategoryReturnCoursesWithLectures(string categoryName, int expected)
        {
            var categoryDevelopment = (Category)Enum.Parse(typeof(Category), "Програмиране");
            var categoryMarkiting = (Category)Enum.Parse(typeof(Category), "Маркетинг");

            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                },
                 new Lecture
                {
                    Id = 2,
                }
            };

            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Category = categoryDevelopment,
                    Lectures = lectures,
                    Status = true

                },
                new Course
                {
                    Id = 2,
                    Category = categoryDevelopment,
                     Status = true
                },
                new Course
                {
                    Id = 3,
                    Category = categoryMarkiting,
                     Status = true
                },
            };

            this.db.Courses.AddRange(courses);
            this.db.SaveChanges();

            var getLecture = this.coursesService.GetCoursesByCategory(categoryName)
                .Select(x => x.Lectures.Count)
                .Sum();

            Assert.Equal(expected, getLecture);
        }
    }
}
