using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Courses;
using CoursesP2P.Tests.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoursesP2P.Tests.Services.Courses
{
    public class CoursesServiceGetAllCoursesTests
    {
        private CoursesP2PDbContext db;
        private IMapper mapper;
        private UserManager<User> userManager;
        private CoursesService coursesService;

        public CoursesServiceGetAllCoursesTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            this.userManager = UserManagerMock.UserManagerMockTest();
            this.coursesService = new CoursesService(this.db, this.mapper, this.userManager);
        }

        [Fact]
        public void GetAllCoursesReturnAllCourses()
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

            this.db.Courses.AddRange(course);
            this.db.SaveChanges();

            var getCourses = this.coursesService.GetAllCourses().ToList();

            Assert.Equal(2, getCourses.Count);
        }

        [Fact]
        public void GetAllCoursesReturnAllCoursesWithAllLectures()
        {
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

            this.db.Courses.AddRange(course);
            this.db.SaveChanges();

            var getCourses = this.coursesService.GetAllCourses()
                .Select(x => x.Lectures.Count).Sum();

            Assert.Equal(3, getCourses);
        }
    }
}
