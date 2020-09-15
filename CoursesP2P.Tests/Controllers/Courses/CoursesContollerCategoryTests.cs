using CoursesP2P.App.Controllers;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Courses;
using CoursesP2P.Services.Reviews;
using CoursesP2P.Tests.Configuration;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace CoursesP2P.Tests.Controllers.Courses
{

    public class CoursesContollerCategoryTests
    {
        private CoursesP2PDbContext db;
        private ICoursesService coursesService;
        private UserManager<User> userManager;
        private CoursesController coursesController;
        private IReviewService reviewService;

        public CoursesContollerCategoryTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.userManager = UserManagerMock.UserManagerMockTest();

            this.coursesService = new CoursesService(db);
            this.coursesController = new CoursesController(coursesService, reviewService, userManager, null, null);
        }

        [Theory]
        [InlineData("Програмиране")]
        public void CategoryReturnCorrectly(string categoryName)
        {
            var isValid = this.coursesController.Category(categoryName);

            Assert.NotNull(isValid);
        }
    }
}
