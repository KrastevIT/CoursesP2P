﻿using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Courses;
using CoursesP2P.Tests.Configuration;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoursesP2P.Tests.Services.Courses
{
    public class CoursesServiceGetCoursesByCategoryTests
    {
        private CoursesP2PDbContext db;
        private CoursesService coursesService;

        public CoursesServiceGetCoursesByCategoryTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var cloudinary = new Mock<ICloudinaryService>().Object;

            this.coursesService = new CoursesService(this.db, cloudinary);
        }

        [Theory]
        [InlineData("Development", 2)]
        [InlineData("Marketing", 1)]
        public void GetCoursesByCategoryReturnCourseByCategory(string categoryName, int expected)
        {
            var categoryDevelopment = (Category)Enum.Parse(typeof(Category), "Development");
            var categoryMarkiting = (Category)Enum.Parse(typeof(Category), "Marketing");

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

            this.db.Courses.AddRange(courses);
            this.db.SaveChanges();

            var getCourses = this.coursesService.GetCoursesByCategory(categoryName);

            Assert.Equal(expected, getCourses.Count());
        }

        [Theory]
        [InlineData("InvalidCategory")]
        public void GetCoursesByCategoryWithNonExistentCategoryReturnException(string categoryName)
        {
            var categoryDevelopment = (Category)Enum.Parse(typeof(Category), "Development");
            var categoryMarkiting = (Category)Enum.Parse(typeof(Category), "Marketing");

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

            this.db.Courses.AddRange(courses);
            this.db.SaveChanges();


            Assert.Throws<InvalidCastException>(() => this.coursesService.GetCoursesByCategory(categoryName));
        }

        [Theory]
        [InlineData("Development", 2)]
        public void GetCoursesByCategoryReturnCoursesWithLectures(string categoryName, int expected)
        {
            var categoryDevelopment = (Category)Enum.Parse(typeof(Category), "Development");
            var categoryMarkiting = (Category)Enum.Parse(typeof(Category), "Marketing");

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
                    Lectures = lectures
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

            this.db.Courses.AddRange(courses);
            this.db.SaveChanges();

            var getLecture = this.coursesService.GetCoursesByCategory(categoryName)
                .Select(x => x.Lectures.Count)
                .Sum();

            Assert.Equal(expected, getLecture);
        }
    }
}
