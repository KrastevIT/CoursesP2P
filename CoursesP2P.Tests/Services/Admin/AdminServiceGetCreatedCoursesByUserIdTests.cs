﻿using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Admin;
using CoursesP2P.Services.Mapping;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CoursesP2P.Tests.Services.Admin
{
    public class AdminServiceGetCreatedCoursesByUserIdTests
    {
        private CoursesP2PDbContext db;
        private AdminService adminService;

        public AdminServiceGetCreatedCoursesByUserIdTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.adminService = new AdminService(db);
        }

        [Theory]
        [InlineData("1")]
        public async Task GetCreatedCoursesByUserIdReturnCreatedCourses(string id)
        {
            var course = new List<Course>
            {
                new Course
                {
                    Id = 1
                },
                 new Course
                {
                    Id = 2
                }
            };

            var users = new User
            {
                Id = "1",
                CreatedCourses = course
            };

           await this.db.Users.AddAsync(users);
           await this.db.SaveChangesAsync();

            var testUsers = this.adminService.GetCreatedCoursesByUserId(id);

            var actual = testUsers.Count();

            Assert.Equal(2, actual);
        }
    }
}
