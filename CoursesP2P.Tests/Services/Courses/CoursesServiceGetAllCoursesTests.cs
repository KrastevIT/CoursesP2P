using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Courses;
using CoursesP2P.Tests.Configuration;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoursesP2P.Tests.Services.Courses
{
    public class CoursesServiceGetAllCoursesTests
    {
        private CoursesP2PDbContext db;
        private CoursesService coursesService;

        public CoursesServiceGetAllCoursesTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var cloudinary = new Mock<ICloudinaryService>().Object;

            this.coursesService = new CoursesService(this.db, null);
        }

        [Fact]
        public async Task GetAllCoursesReturnAllCourses()
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

            await this.db.Courses.AddRangeAsync(course);
            await this.db.SaveChangesAsync();

            var getCourses = this.coursesService.GetАpprovedCourses().ToList();

            Assert.Equal(2, getCourses.Count);
        }

        [Fact]
        public async Task GetAllCoursesReturnAllCoursesWithAllLectures()
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

           await this.db.Courses.AddRangeAsync(course);
           await this.db.SaveChangesAsync();

            var getCourses = this.coursesService.GetАpprovedCourses()
                .Select(x => x.Lectures.Count).Sum();

            Assert.Equal(3, getCourses);
        }
    }
}
