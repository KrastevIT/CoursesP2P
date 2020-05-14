using CoursesP2P.App.Controllers;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Reviews;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels.Reviews;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CoursesP2P.Tests.Controllers.Reviews
{
    public class ReviewsControllerAddTests
    {
        private CoursesP2PDbContext db;
        private UserManager<User> userManager;
        private ReviewsController reviewsController;
        private ReviewService reviewService;

        public ReviewsControllerAddTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.userManager = UserManagerMock.UserManagerMockTest();
            this.reviewService = new ReviewService(db);

            this.reviewsController = new ReviewsController(null, reviewService, userManager);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]

        public async Task AddReturnInvalid(int courseId)
        {
            var course = new List<Course>
            {
               new Course
               {
                    Id = 1,
                    Review = new Review { Id = 1 }
               },
               new Course
               {
                    Id = 2,
               }
            };

            var model = new ReviewBindingModel
            {
                CourseId = courseId
            };

            await this.db.Courses.AddRangeAsync(course);
            await this.db.SaveChangesAsync();



            var actionResult = await this.reviewsController.Add(model);

            var jsonResult = Assert.IsType<JsonResult>(actionResult);

            Assert.Equal("invalid", jsonResult.Value);
        }
    }
}
