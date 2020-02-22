using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Instructors;
using CoursesP2P.Tests.Configuration;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoursesP2P.Tests.Services.Instructors
{
    public class InstructorsServiceGetCreatedCoursesTests
    {
        private CoursesP2PDbContext db;
        private IMapper mapper;
        private InstructorsService instructorsService;

        public InstructorsServiceGetCreatedCoursesTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            this.instructorsService = new InstructorsService(db, mapper);
        }

        [Fact]
        public void GetCreatedCoursesReturnCorrectly()
        {
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                },
                new Course
                {
                    Id = 2
                }
            };

            var user = new User
            {
                Id = "1",
                CreatedCourses = courses
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();


            var actual = this.instructorsService.GetCreatedCourses(user).Courses.Count();

            Assert.Equal(2, actual);
        }
    }
}
