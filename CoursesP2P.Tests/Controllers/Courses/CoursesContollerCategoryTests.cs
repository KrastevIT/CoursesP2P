using AutoMapper;
using CoursesP2P.App.Controllers;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Courses;
using CoursesP2P.Tests.Configuration;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CoursesP2P.Tests.Controllers.Courses
{

    public class CoursesContollerCategoryTests
    {
        private CoursesP2PDbContext db;
        private ICoursesService coursesService;
        private readonly IMapper mapper;
        private UserManager<User> userManager;
        private CoursesController coursesController;

        public CoursesContollerCategoryTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.userManager = UserManagerMock.UserManagerMockTest();
            this.mapper = MapperMock.AutoMapperMock();

            this.coursesService = new CoursesService(db, mapper, userManager);
            this.coursesController = new CoursesController(coursesService, userManager);
        }

        [Theory]
        [InlineData("Development")]
        public void CategoryReturnCorrectly(string categoryName)
        {
            var isValid = this.coursesController.Category(categoryName);

            Assert.NotNull(isValid);
        }
    }
}
