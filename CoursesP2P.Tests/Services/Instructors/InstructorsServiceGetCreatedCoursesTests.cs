using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Instructors;
using CoursesP2P.Services.Mapping;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CoursesP2P.Tests.Services.Instructors
{
    public class InstructorsServiceGetCreatedCoursesTests
    {
        private CoursesP2PDbContext db;
        private InstructorsService instructorsService;

        public InstructorsServiceGetCreatedCoursesTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.instructorsService = new InstructorsService(db);
        }

        [Fact]
        public async Task GetCreatedCoursesReturnCorrectly()
        {
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                     Status = true
                },
                new Course
                {
                    Id = 2,
                     Status = true
                }
            };

            var user = new User
            {
                Id = "1",
                CreatedCourses = courses
            };

            await this.db.Users.AddAsync(user);
            await this.db.SaveChangesAsync();


            var actual = this.instructorsService.GetCreatedCourses("1").Count();

            Assert.Equal(2, actual);
        }

        [Fact]
        public async Task GetCreatedCoursesWithLecturesReturnCorrectly()
        {
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1
                },
                new Lecture
                {
                    Id = 2
                }
            };

            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Lectures = lectures
                },
            };

            var user = new User
            {
                Id = "1",
                CreatedCourses = courses
            };

            await this.db.Users.AddAsync(user);
            await this.db.SaveChangesAsync();


            var actual = this.instructorsService.GetCreatedCourses("1").Count();

            Assert.Equal(1, actual);
        }
    }
}
