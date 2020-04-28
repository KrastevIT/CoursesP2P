using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Courses;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels.Courses.BindingModels;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoursesP2P.Tests.Services.Courses
{
    public class CoursesServiceCreateAsyncTests
    {
        private CoursesP2PDbContext db;
        private CoursesService coursesService;

        public CoursesServiceCreateAsyncTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var cloudinary = new Mock<ICloudinaryService>().Object;
            this.coursesService = new CoursesService(this.db, cloudinary);
        }

        [Fact]
        public async Task CreateCourseShouldCorrectly()
        {
            var categoryDevelopment = (CategoryViewModel)Enum.Parse(typeof(CategoryViewModel), "Development");

            var image = new Mock<IFormFile>().Object;

            var user = new User
            {
                Id = "1",
                FirstName = "Pesho",
                LastName = "Ivanov"
            };

            var model = new CreateCourseBindingModel
            {
                Name = "Kris",
                Description = "qwqwqwqwqwqwqwqwqwqwqwqwq",
                Price = 50,
                Skills = "test",
                Category = categoryDevelopment,
                Image = image,
                InstructorFullName = "InstructorName",
                InstructorId = "1"
            };

            await this.coursesService.CreateAsync(model, user);

            int actual = db.Courses.ToList().Count();

            Assert.Equal(1, actual);
        }


        [Fact]
        public async Task CreateCourseWithInvalidNameReturnException()
        {
            var categoryDevelopment = (CategoryViewModel)Enum.Parse(typeof(CategoryViewModel), "Development");

            var image = new Mock<IFormFile>().Object;

            var user = new User
            {
                Id = "1",
                FirstName = "Pesho",
                LastName = "Ivanov"
            };

            var model = new CreateCourseBindingModel
            {
                Name = null,
                Description = null,
                Price = 50,
                Skills = null,
                Category = categoryDevelopment,
                Image = image,
                InstructorFullName = "InstructorName",
                InstructorId = "1"
            };

            int actual = db.Courses.ToList().Count();

            await Assert.ThrowsAsync<InvalidOperationException>(() => this.coursesService.CreateAsync(model, user));
        }
    }
}
