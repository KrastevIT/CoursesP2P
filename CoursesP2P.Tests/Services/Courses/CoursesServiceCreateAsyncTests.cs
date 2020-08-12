using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using CoursesP2P.Services.Courses;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels.Courses.BindingModels;
using Microsoft.AspNetCore.Http;
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
            this.coursesService = new CoursesService(this.db);
        }

        [Fact]
        public async Task CreateCourseShouldCorrectly()
        {
            var categoryDevelopment = (Category)Enum.Parse(typeof(Category), "Програмиране");

            var image = new Mock<IFormFile>().Object;

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

            await this.coursesService.CreateAsync(model, "1", "Pesho", "Ivanov", "imageUrl");

            int actual = db.Courses.ToList().Count();

            Assert.Equal(1, actual);
        }
    }
}
