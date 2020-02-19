using AutoMapper;
using CoursesP2P.App.Mapping;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Courses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CoursesP2P.Tests
{
    public class CoursesServiceTests
    {
        [Fact]
        public void GetAllCoursesReturnAllCourses()
        {
            var optionBuilder = new DbContextOptionsBuilder<CoursesP2PDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new CoursesP2PDbContext(optionBuilder.Options);

            var mockMapper = new MapperConfiguration(x =>
            {
                x.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var userManager = new Mock<UserManager<User>>(
                    new Mock<IUserStore<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<User>>().Object,
                    new IUserValidator<User>[0],
                    new IPasswordValidator<User>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<User>>>().Object).Object;

            var coursesService = new CoursesService(db, mapper, userManager);

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

            db.Courses.AddRange(course);
            db.SaveChanges();

            var getCourses = coursesService.GetAllCourses().ToList();

            Assert.Equal(2, getCourses.Count);
        }

        [Fact]
        public void GetAllCoursesReturnAllCoursesWithAllLectures()
        {
            var optionBuilder = new DbContextOptionsBuilder<CoursesP2PDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new CoursesP2PDbContext(optionBuilder.Options);

            var mockMapper = new MapperConfiguration(x =>
            {
                x.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var userManager = new Mock<UserManager<User>>(
                    new Mock<IUserStore<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<User>>().Object,
                    new IUserValidator<User>[0],
                    new IPasswordValidator<User>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<User>>>().Object).Object;

            var coursesService = new CoursesService(db, mapper, userManager);

            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1
                },
                new Lecture
                {
                    Id= 2
                },
                new Lecture
                {
                    Id= 3
                }
            };

            var course = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Lectures = lectures
                },
                new Course
                {
                    Id = 2
                }
            };

            db.Courses.AddRange(course);
            db.SaveChanges();

            var getCourses = coursesService.GetAllCourses()
                .Select(x => x.Lectures.Count).Sum();

            Assert.Equal(3, getCourses);
        }
    }
}
