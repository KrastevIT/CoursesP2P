using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Courses;
using CoursesP2P.Tests.Configuration;
using Microsoft.AspNetCore.Identity;

namespace CoursesP2P.Tests.Services.Courses
{
    public class CoursesServiceCreateAsyncTests
    {
        private CoursesP2PDbContext db;
        private readonly IMapper mapper;
        private UserManager<User> userManager;
        private CoursesService coursesService;

        public CoursesServiceCreateAsyncTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            this.userManager = UserManagerMock.UserManagerMockTest();
            this.coursesService = new CoursesService(this.db, this.mapper, this.userManager);
        }
    }
}
