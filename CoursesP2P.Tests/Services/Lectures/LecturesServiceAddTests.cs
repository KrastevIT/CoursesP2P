﻿using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Lectures;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels.Lectures.BindingModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoursesP2P.Tests.Services.Lectures
{
    public class LecturesServiceAddTests
    {
        private CoursesP2PDbContext db;
        private LecturesService lecturesService;

        public LecturesServiceAddTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var cloudinary = new Mock<ICloudinaryService>().Object;


            this.lecturesService = new LecturesService(this.db);
        }

        [Fact]
        public async Task AddReturnCorrectly()
        {
            var video = new Mock<IFormFile>().Object;

            var model = new AddLecturesBindingModel
            {
                Name = "FirstLecture",
                CourseId = 1,
                Video = video
            };

            //await this.lecturesService.AddAsync(model);

            var actual = this.db.Lectures.Count();

            Assert.Equal(1, actual);
        }
    }
}
