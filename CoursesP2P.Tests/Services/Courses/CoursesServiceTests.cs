using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using CoursesP2P.Services.Courses;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels.Courses.BindingModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace CoursesP2P.Tests.Services.Courses
{
    public class CoursesServiceTests
    {
        private CoursesP2PDbContext db;
        private IMapper mapper;
        private UserManager<User> userManager;

        public CoursesServiceTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            this.userManager = UserManagerMock.UserManagerMockTest();
        }

       

       

        

        [Fact]
        public void CreateCourse()
        {

        }
    }
}
