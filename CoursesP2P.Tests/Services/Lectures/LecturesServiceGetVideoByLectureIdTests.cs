﻿using AutoMapper;
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
using System.Linq;
using System.Reflection;
using System.Text;
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

            var model = this.lecturesService.GetVideoByLectureId(id, "1");

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

            Assert.Throws<ArgumentNullException>(() => this.lecturesService.GetVideoByLectureId(id, "1"));
        }
    }
}
