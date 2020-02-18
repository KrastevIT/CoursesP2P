using AutoMapper;
using CoursesP2P.App.Mapping;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Admin;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoursesP2P.Tests
{
    public class AdminServiceTests
    {
        [Fact]
        public void GetUsersShouldReturnAllUsers()
        {
            var users = new List<User>
            {
                new User
                {
                    Id = "1",
                },
                new User
                {
                    Id = "2",
                }
            };

            var optionBuilder = new DbContextOptionsBuilder<CoursesP2PDbContext>()
                  .UseInMemoryDatabase("name");
            var db = new CoursesP2PDbContext(optionBuilder.Options);

            db.Users.AddRange(users);
            db.SaveChanges();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            var mapper = mockMapper.CreateMapper();

            var adminService = new AdminService(db, mapper);

            var testUsers = adminService.GetUsers().ToList();

            Assert.Equal(2, testUsers.Count());

        }
    }
}
