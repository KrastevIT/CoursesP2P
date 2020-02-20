using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Admin;
using CoursesP2P.Tests.Configuration;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoursesP2P.Tests.Services.Admin
{
    public class AdminServiceGetCreatedCoursesByUserIdTests
    {
        private CoursesP2PDbContext db;
        private IMapper mapper;
        private AdminService adminService;

        public AdminServiceGetCreatedCoursesByUserIdTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            this.adminService = new AdminService(db, mapper);
        }

        [Theory]
        [InlineData("1")]
        public void GetCreatedCoursesByUserIdReturnCreatedCourses(string id)
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

            this.db.Users.Add(users);
            this.db.SaveChanges();

            var testUsers = this.adminService.GetCreatedCoursesByUserId(id);

            var actual = testUsers.Count();

            Assert.Equal(2, actual);
        }
    }
}
